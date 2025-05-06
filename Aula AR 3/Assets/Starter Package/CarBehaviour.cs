/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using TMPro;
using UnityEngine;

/**
 * Our car will track a reticle and collide with a <see cref="PackageBehaviour"/>.
 */
public class CarBehaviour : MonoBehaviour
{
    public ReticleBehaviour Reticle;
    public float Speed = 1.2f;

    public int pontos = 0;
    public TextMeshProUGUI txtPontos;
    

    private void Awake()
    {
        txtPontos = GameObject.Find("TxtPontos").GetComponent<TextMeshProUGUI>();
        txtPontos.text = "Pontos: " + pontos.ToString();
    }

    private void Update()
    {
        var trackingPosition = Reticle.transform.position;
        if (Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position =
            Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        if (Package != null)
        {
            if (Package.gameObject.CompareTag("package1"))
            {
                pontos += 10;
            }
            else if (Package.gameObject.CompareTag("package2"))
            {
                pontos += 20;
            }
            else if (Package.gameObject.CompareTag("package3"))
            {
                pontos += 30;
            }
            else if (Package.gameObject.CompareTag("package4"))
            {
                pontos += 40;
            }
            Debug.Log(Package.gameObject.name + " pontos: " + pontos);
            txtPontos.text = "Pontos: " + pontos.ToString();
            Destroy(other.gameObject);
            if (pontos >= 100)
            {
                pontos = 100;
                WinCondition winCon = GameObject.Find("WInLoseCondition").GetComponent<WinCondition>();
                winCon.pnlVitoria.SetActive(true);
            }
            else if (Package.gameObject.CompareTag("obstaculo"))
            {
                Debug.Log("Colidiu com: " + Package.name);
                WinCondition winCon = GameObject.Find("WInLoseCondition").GetComponent<WinCondition>();
                winCon.pnlDerrota.SetActive(true);

                //Destroy(Package.gameObject);
                Destroy(gameObject);

            }
        }
    }
}
