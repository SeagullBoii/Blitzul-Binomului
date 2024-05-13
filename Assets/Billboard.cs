using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Directions directions = Directions.OneDir;
    [SerializeField] Animator animator;

    //Cate directii are sprite-ul
    enum Directions 
    { 
        OneDir,
        FourDir,
        EightDir
    }

    private void Update()
    {
        LookAtCam();
    }

    private void FixedUpdate()
    {
        if (directions != Directions.OneDir)
            Angle();
    }

    /// <summary>
    /// Facem sprite-ul sa se uite mereu in directia camerei.
    /// </summary>
    private void LookAtCam() {
        Vector3 cameraDir = Camera.main.transform.forward;
        cameraDir.y = 0;
        spriteRenderer.transform.rotation = Quaternion.LookRotation(cameraDir);
    }

    /// <summary>
    /// Luam unghiul de la obiect la jucator pentru a modifica sprite-ul in functie de pozitia jucatorului (Front, left, right etc)
    /// Proiectam unghiul pe un plan bidimensional (X, Z), de aceea </code> targetPos.y = transform.position.y <code>
    /// La final modificam variabila "Direction" din animator in functie de unghi.
    /// </summary>
    private void Angle() {
        Vector3 targetPos = Camera.main.transform.position;
        targetPos.y = transform.position.y;
        Vector3 targetDir = transform.position - targetPos;
        float angleToCam = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        animator.SetFloat("Direction", GetIndex(angleToCam, Directions.EightDir));
    }

    /// <summary>
    /// Utilizam Blend Tree in animator pentru a modifica cu usurinta sprite-ul.
    /// Indexul reprezitna variabila "Direction" din Animator, fiecare valoare de la 0-7 (pentru 8 directii) sau 0-3 (pentru 4 directii) reprezinta un unghi diferit.
    /// Daca are o singura directie, folosim indexul 0;
    /// </summary>
    private int GetIndex(float angle, Directions dir) {
        switch (dir) {
            case Directions.EightDir:
                if (angle > -22.5f && angle < 22.5f)
                    return 0;
                else if (angle >= 22.5f && angle < 67.5f)
                    return 1;
                else if (angle >= 67.5f && angle < 112.5f)
                    return 2;
                else if (angle >= 112.5f && angle < 157.5f)
                    return 3;
                else if (angle <= -157.5f || angle >= 157.5f)
                    return 7;
                else if (angle >= -157.5f && angle < -112.5f)
                    return 6;
                else if (angle >= -112.5f && angle < -67.5f)
                    return 5;
                else
                    return 4;

            case Directions.FourDir:
                if (angle > -45f && angle < 45f)
                    return 0;
                else if (angle >= 45f && angle < 90f)
                    return 1;
                else if (angle >= 90f && angle < 135f)
                    return 3;
                else
                    return 2;
        }
        return 0;
    }

}
