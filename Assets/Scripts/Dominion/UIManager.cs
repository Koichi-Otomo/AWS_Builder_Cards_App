using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dominion
{
    public class UIManager : MonoBehaviour
    {
        public GameManager gameManager;
        Canvas canvas;
        RectTransform handPanel;
        List<GameObject> buttons = new List<GameObject>();

        void Start()
        {
            if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
            CreateUIIfNeeded();
            InvokeRepeating(nameof(RefreshHand), 0.1f, 0.5f);
        }

        void CreateUIIfNeeded()
        {
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var go = new GameObject("Canvas");
                canvas = go.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                go.AddComponent<CanvasScaler>();
                go.AddComponent<GraphicRaycaster>();
            }

            var handGO = new GameObject("HandPanel");
            handGO.transform.SetParent(canvas.transform);
            handPanel = handGO.AddComponent<RectTransform>();
            handPanel.anchorMin = new Vector2(0.1f, 0f);
            handPanel.anchorMax = new Vector2(0.9f, 0.25f);
            handPanel.offsetMin = Vector2.zero;
            handPanel.offsetMax = Vector2.zero;
            var img = handGO.AddComponent<Image>();
            img.color = new Color(0f, 0f, 0f, 0.25f);
        }

        void RefreshHand()
        {
            if (gameManager == null || gameManager.players.Count == 0) return;
            var player = gameManager.players[gameManager.currentPlayerIndex];

            // clear old
            foreach (var b in buttons) Destroy(b);
            buttons.Clear();

            // create buttons for each card in hand
            for (int i = 0; i < player.Hand.Count; i++)
            {
                var card = player.Hand[i];
                var b = CreateCardButton(card, i, player);
                buttons.Add(b);
            }
        }

        GameObject CreateCardButton(Card card, int index, Player player)
        {
            var go = new GameObject("CardButton_") { layer = LayerMask.NameToLayer("UI") };
            go.transform.SetParent(handPanel);
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = new Vector2((float)index / 10f, 0f);
            rt.anchorMax = new Vector2(((float)index + 1f) / 10f, 1f);
            rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;

            var btn = go.AddComponent<Button>();
            var img = go.AddComponent<Image>();
            img.color = Color.white * 0.9f;

            var txtGO = new GameObject("Text"); txtGO.transform.SetParent(go.transform);
            var txtRT = txtGO.AddComponent<RectTransform>(); txtRT.anchorMin = Vector2.zero; txtRT.anchorMax = Vector2.one; txtRT.offsetMin = Vector2.zero; txtRT.offsetMax = Vector2.zero;
            var txt = txtGO.AddComponent<Text>(); txt.alignment = TextAnchor.MiddleCenter; txt.color = Color.black; txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); txt.text = card.Name + " (" + card.Cost + ")";

            btn.onClick.AddListener(() => OnCardClicked(card, player));

            return go;
        }

        void OnCardClicked(Card card, Player player)
        {
            if (player.PlayCard(card, gameManager))
            {
                Debug.Log($"{player.Name} played {card.Name} via UI");
                RefreshHand();
            }
        }
    }

}
