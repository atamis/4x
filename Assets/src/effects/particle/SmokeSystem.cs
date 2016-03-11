using UnityEngine;

namespace game.effects {
    class SmokeEffect : MonoBehaviour {
        ParticleSystem ps;
		ParticleSystemRenderer pr;

        void Start() {
            ps = gameObject.AddComponent<ParticleSystem>();
			pr = ps.GetComponent<ParticleSystemRenderer>();

			Material mat = new Material (Shader.Find ("Sprites/Default"));
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/p_large");
			pr.material = mat;

			// shape
			var sh = ps.shape;
			sh.enabled = true;
			sh.shapeType = ParticleSystemShapeType.Cone;
			sh.angle = 1;
			sh.radius = 0.1f;

			// size
			var sz = ps.sizeOverLifetime;
			sz.enabled = true;
			AnimationCurve curve = new AnimationCurve ();
			curve.AddKey (0.05f, 0.05f);
			curve.AddKey (0.7f, 0.7f);
			sz.size = new ParticleSystem.MinMaxCurve (1.0f, curve);

			// start speed
			ps.startSpeed = .1f;

			// gravity
			ps.gravityModifier = -0.02f;
        }
    }
}
