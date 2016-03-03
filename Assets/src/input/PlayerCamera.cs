using UnityEngine;
using System.Collections;

namespace game.input {
    public class PlayerCamera : MonoBehaviour {

        Camera cam;
        float speed = 1f;

        public void init(Camera cam) {
            this.cam = cam;

            cam.transform.parent = transform;
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            var scroll = Input.GetAxis("Mouse ScrollWheel");


            // Scroll up is a positive change, but increasing size
            // zooms out, so we subtract.
            cam.orthographicSize -= scroll;

            // Ensure 1 <= size <= 20.
            cam.orthographicSize = System.Math.Min(System.Math.Max(1, cam.orthographicSize), 20);

            var control = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.A)) {
                control.x -= speed;
            }

            if (Input.GetKey(KeyCode.D)) {
                control.x += speed;
            }

            if (Input.GetKey(KeyCode.W)) {
                control.y += speed;
            }

            if (Input.GetKey(KeyCode.S)) {
                control.y -= speed;
            }

            // Include zoom-level to make zoomed-out movement faster.
            transform.localPosition += control * Time.deltaTime * cam.orthographicSize;


        }
    }
}
