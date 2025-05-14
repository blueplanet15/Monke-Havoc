using GorillaLocomotion;
using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Libraries
{
    internal class Gun
    {
        private LineRenderer lineRenderer;
        private GameObject pointerBall;
        public RaycastHit hit;

        public void OnEnable()
        {
            pointerBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointerBall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            GameObject.Destroy(pointerBall.GetComponent<Collider>());
            pointerBall.SetActive(false);

            lineRenderer = pointerBall.AddComponent<LineRenderer>();
            lineRenderer.material.shader = Shader.Find("GorillaTag/UberShader");
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        public void UpdateGun()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (Physics.Raycast(GTPlayer.Instance.rightControllerTransform.position,
                        -GTPlayer.Instance.rightControllerTransform.up, out RaycastHit hitt, Mathf.Infinity,
                        GTPlayer.Instance.locomotionEnabledLayers))
                {
                    if (!pointerBall.activeSelf)
                    {
                        pointerBall.SetActive(true);
                    }

                    if (!lineRenderer.enabled)
                    {
                        lineRenderer.enabled = true;
                    }

                    hit = hitt;
                    pointerBall.transform.position = hit.point;
                    lineRenderer.SetPositions(new Vector3[]
                        { GTPlayer.Instance.rightControllerTransform.position, pointerBall.transform.position });
                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                    {
                        pointerBall.GetComponent<Renderer>().material.color = Plugin.otherpurp;
                        lineRenderer.material.color = Plugin.otherpurp;
                    }
                    else
                    {
                        pointerBall.GetComponent<Renderer>().material.color = Plugin.purp;
                        lineRenderer.material.color = Plugin.purp;
                    }
                }
            }
            else
            {
                if (pointerBall.activeSelf)
                {
                    pointerBall.SetActive(false);
                }

                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
        }

        public void OnDisable()
        {
            GameObject.Destroy(pointerBall);
        }
    }
}