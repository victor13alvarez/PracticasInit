using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCanvas_EndGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextComponent;
    [SerializeField] private float FadeSpeed = 20.0f;
    [SerializeField] private int RolloverCharacterSpread = 10;
    [SerializeField] private GameObject _PlayerWon;
    TextMeshProUGUI _playerWonText;
    const string _hasWin = " HAS WIN";

    private void Awake()
    {
        _playerWonText = _PlayerWon.GetComponent<TextMeshProUGUI>();
        _playerWonText.text = "";

    }
    public void FadeInTransitionComplete()
    {
        StartCoroutine(FadeInText());
        PlayerHasWon();
    }

    void PlayerHasWon()
    {
        _playerWonText.text = ArcheryGameManager._playerHasWin + _hasWin;
        StartCoroutine(ScaleUpAndDown(_PlayerWon.transform, Vector3.one, 1f));
    }



    IEnumerator ScaleUpAndDown(Transform transform, Vector3 upScale, float duration)
    {
        Vector3 initialScale = transform.localScale;

        for (float time = 0; time < duration * 2; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, duration) / duration;
            transform.localScale = Vector3.Lerp(initialScale, upScale, progress);
            yield return null;
        }
        transform.localScale = initialScale;
        if(transform.localScale == Vector3.one) StartCoroutine(ScaleUpAndDown(_PlayerWon.transform, Vector3.one, 1f));
        else StartCoroutine(ScaleUpAndDown(_PlayerWon.transform, Vector3.one /2, 1f));

    }
    // ...
    /// <summary>
    /// Method to animate (fade in) vertex colors of a TMP Text object.
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeInText()
    {
        // Set the whole text transparent
        m_TextComponent.color = new Color
            (
                m_TextComponent.color.r,
                m_TextComponent.color.g,
                m_TextComponent.color.b,
                0
            );
        // Need to force the text object to be generated so we have valid data to work with right from the start.
        m_TextComponent.ForceMeshUpdate();


        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        Color32[] newVertexColors;

        int currentCharacter = 0;
        int startingCharacterRange = currentCharacter;
        bool isRangeMax = false;

        while (!isRangeMax)
        {
            int characterCount = textInfo.characterCount;

            // Spread should not exceed the number of characters.
            byte fadeSteps = (byte)Mathf.Max(1, 255 / RolloverCharacterSpread);

            for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
            {
                // Skip characters that are not visible (like white spaces)
                if (!textInfo.characterInfo[i].isVisible) continue;

                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                // Get the vertex colors of the mesh used by this text element (character or sprite).
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // Get the current character's alpha value.
                byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a + fadeSteps, 0, 255);

                // Set new alpha values.
                newVertexColors[vertexIndex + 0].a = alpha;
                newVertexColors[vertexIndex + 1].a = alpha;
                newVertexColors[vertexIndex + 2].a = alpha;
                newVertexColors[vertexIndex + 3].a = alpha;

                if (alpha == 255)
                {
                    startingCharacterRange += 1;

                    if (startingCharacterRange == characterCount)
                    {
                        // Update mesh vertex data one last time.
                        m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                        yield return new WaitForSeconds(1.0f);

                        // Reset the text object back to original state.
                        m_TextComponent.ForceMeshUpdate();

                        yield return new WaitForSeconds(1.0f);

                        // Reset our counters.
                        currentCharacter = 0;
                        startingCharacterRange = 0;
                        //isRangeMax = true; // Would end the coroutine.
                    }
                }
            }

            // Upload the changed vertex colors to the Mesh.
            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            if (currentCharacter + 1 < characterCount) currentCharacter += 1;

            yield return new WaitForEndOfFrame();
        }
    }

    internal void FadeOutTransitionComplete()
    {
        StopAllCoroutines();
    }
}
