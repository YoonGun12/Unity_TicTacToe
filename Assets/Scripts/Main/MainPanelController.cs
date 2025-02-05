using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class MainPanelController : MonoBehaviour
{
    public void OnClickSinglePlayButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnClickDualPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnClickSettingsButton()
    {
        
    }
    
    
    public TMP_Text tmpText;
    public float jumpHeight = 30f;  // 점프 높이
    public float duration = 0.5f;   // 점프 시간
    public float delayBetweenLetters = 0.1f;  // 글자별 딜레이

    private TMP_TextInfo textInfo;

    void Start()
    {
        if (tmpText == null)
            tmpText = GetComponent<TMP_Text>();

        textInfo = tmpText.textInfo;
        StartCoroutine(AnimateJumpingText());
    }

    IEnumerator AnimateJumpingText()
    {
        tmpText.ForceMeshUpdate(); // 텍스트 정보를 강제로 업데이트
        Vector3[] originalVertices = new Vector3[textInfo.meshInfo[0].vertices.Length];

        for (int i = 0; i < originalVertices.Length; i++)
        {
            originalVertices[i] = textInfo.meshInfo[0].vertices[i];
        }

        while (true)
        {
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                // 기존 위치 저장
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                // DOTween을 사용하여 점프 애니메이션 적용
                DOTween.To(
                    () => vertices[vertexIndex + 0].y,
                    y => 
                    {
                        float offset = y - vertices[vertexIndex + 0].y;
                        vertices[vertexIndex + 0] += new Vector3(0, offset, 0);
                        vertices[vertexIndex + 1] += new Vector3(0, offset, 0);
                        vertices[vertexIndex + 2] += new Vector3(0, offset, 0);
                        vertices[vertexIndex + 3] += new Vector3(0, offset, 0);
                        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                    },
                    jumpHeight,
                    duration / 2
                ).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    DOTween.To(
                        () => vertices[vertexIndex + 0].y,
                        y =>
                        {
                            float offset = y - vertices[vertexIndex + 0].y;
                            vertices[vertexIndex + 0] += new Vector3(0, offset, 0);
                            vertices[vertexIndex + 1] += new Vector3(0, offset, 0);
                            vertices[vertexIndex + 2] += new Vector3(0, offset, 0);
                            vertices[vertexIndex + 3] += new Vector3(0, offset, 0);
                            tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                        },
                        0,
                        duration / 2
                    ).SetEase(Ease.InQuad);
                });

                yield return new WaitForSeconds(delayBetweenLetters);
            }
            
            yield return new WaitForSeconds(1f); // 한 번 끝난 후 다시 실행할 때 대기 시간
        }
    }
}
