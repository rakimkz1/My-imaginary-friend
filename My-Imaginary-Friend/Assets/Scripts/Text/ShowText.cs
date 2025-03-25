using System.Collections;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public float showSpeed = 1000f, textSpeed = 0.1f;
    public float explosionForce = 20f, explosionRandomness = 5f;

    private Color32 textColor;
    private Coroutine activeCoroutine;
    private Vector3[] velocities;

    private void Awake()
    {
        textColor = textMeshPro.color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (activeCoroutine != null)
                StopCoroutine(activeCoroutine);

            activeCoroutine = StartCoroutine(EndAnimate());
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (activeCoroutine != null)
                StopCoroutine(activeCoroutine);

            activeCoroutine = StartCoroutine(PermanentEnd());
        }
    }


    
    public IEnumerator StartAnimate()
    {
        float currentTime = 0;
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        while (true)
        {
            currentTime += Time.deltaTime;
            bool allVisible = true;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;
                var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

                for (int j = 0; j < 4; j++)
                {
                    int index = charInfo.vertexIndex + j;
                    byte alpha = (byte)Mathf.Clamp((currentTime - (index / 2) * textSpeed) * showSpeed, 0, 255);
                    meshInfo.colors32[index] = new Color32(textColor.r, textColor.g, textColor.b, alpha);

                    if (alpha < 255)
                        allVisible = false;
                }
            }

            ApplyMeshChanges(textInfo);
            if (allVisible) 
                break; 

            yield return null;
        }
    }

    public IEnumerator StartAnimate(string text)
    {
        textMeshPro.text = text;
        StartCoroutine(StartAnimate());
        yield return null;  
    }

    private Vector3[] currentPositions; // Храним позиции символов
    private Color32[] currentColors;    // Храним текущие альфа-значения

    public IEnumerator EndAnimate()
    {
        float currentTime = 0;
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;
        velocities = new Vector3[textInfo.characterCount * 4];

        // Инициализация массивов для передачи в PermanentEnd
        currentPositions = new Vector3[textInfo.characterCount * 4];
        currentColors = new Color32[textInfo.characterCount * 4];

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            float angle = Random.Range(-explosionRandomness, explosionRandomness);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Vector3 force = direction * explosionForce;

            for (int j = 0; j < 4; j++)
                velocities[i * 4 + j] = force;
        }

        while (true)
        {
            currentTime += Time.deltaTime;
            bool allInvisible = true;

            TMP_TextInfo _textInfo = textMeshPro.textInfo;

            for (int i = 0; i < _textInfo.characterCount; i++)
            {
                var charInfo = _textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;
                var meshInfo = _textInfo.meshInfo[charInfo.materialReferenceIndex];
                var verts = meshInfo.vertices;

                for (int j = 0; j < 4; j++)
                {
                    int index = charInfo.vertexIndex + j;

                    byte force = (byte)Mathf.Clamp((currentTime - (index / 4) * textSpeed * 2) * showSpeed * 0.03f, 0, 255);
                    verts[index] += velocities[index] * Time.deltaTime * force * 5;

                    byte alpha = (byte)Mathf.Clamp((((index / 4) + 10) * textSpeed * 2 - currentTime) * showSpeed * 0.5f, 0, 255);
                    meshInfo.colors32[index] = new Color32(textColor.r, textColor.g, textColor.b, alpha);

                    if (alpha > 0)
                        allInvisible = false;

                    // Сохраняем текущие позиции и альфа-канал для плавного перехода в PermanentEnd
                    currentPositions[index] = verts[index];
                    currentColors[index] = meshInfo.colors32[index];
                }
            }

            ApplyMeshChanges(_textInfo);
            if (allInvisible)
                break;

            yield return null;
        }
    }

    public IEnumerator PermanentEnd(float duration = 0.4f)
    {
        if (currentPositions == null || currentColors == null)
        {
            Debug.LogWarning("PermanentEnd вызван до EndAnimate!");
            yield break;
        }

        float currentTime = 0;
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        Vector3[] velocities = new Vector3[textInfo.characterCount * 4];

        // Генерируем хаотичные направления
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            float angle = Random.Range(-explosionRandomness, explosionRandomness);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Vector3 force = direction * explosionForce * 1.5f; // Немного ускоряем разлет

            for (int j = 0; j < 4; j++)
                velocities[i * 4 + j] = force;
        }

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float progress = currentTime / duration; // 0 → 1

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;
                var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];
                var verts = meshInfo.vertices;

                for (int j = 0; j < 4; j++)
                {
                    int index = charInfo.vertexIndex + j;

                    // Начинаем движение с уже существующих позиций
                    verts[index] = currentPositions[index] + velocities[index] * currentTime * (1 + progress * 50) * 5;

                    // Начинаем уменьшение альфа с текущего значения
                    byte startAlpha = currentColors[index].a;
                    byte alpha = (byte)Mathf.Clamp(startAlpha * (1 - progress), 0, 255);
                    meshInfo.colors32[index] = new Color32(textColor.r, textColor.g, textColor.b, alpha);
                }
            }

            ApplyMeshChanges(textInfo);
            yield return null;
        }
    }

    private void ApplyMeshChanges(TMP_TextInfo textInfo)
    {
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            meshInfo.mesh.colors32 = meshInfo.colors32;
            textMeshPro.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
