using System;
using Units;
using UnityEngine;

namespace UI
{
    public class UnitCreator : MonoBehaviour
    {
        public Action<Unit> unitCreateEvent;

        private Vector2 mousePos;
        private Camera mainCamera;
        private SpriteRenderer _spriteRenderer;

        private GameObject unitToCreate;
        private bool creatingUnit;

        public void Initialize()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            mainCamera = Camera.main;
        }

        public void ButtonClicked(GameObject unitToCreate)
        {
            this.unitToCreate = unitToCreate;
            creatingUnit = true;
            _spriteRenderer.sprite = unitToCreate.GetComponent<Unit>().GetSprite();
        }

        public void DisableCreation()
        {
            creatingUnit = false;
            unitToCreate = null;
            _spriteRenderer.sprite = null;
        }

        private void Update()
        {
            if (!creatingUnit) return;

            if (Input.GetMouseButtonDown(1))
            {
                DisableCreation();
                return;
            }

            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity);

                if (rayHit.collider == null)
                {
                    GameObject newUnit = Instantiate(unitToCreate, transform.position, Quaternion.identity);
                    unitCreateEvent.Invoke(newUnit.GetComponent<Unit>());
                }
            }
        }
    }
}