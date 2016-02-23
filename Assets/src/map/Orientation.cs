using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.map {
    struct Orientation {
        public float f0, f1, f2, f3;
        public float b0, b1, b2, b3;
        public float angle;
        
        private Orientation(float f0, float f1, float f2, float f3,
            float b0, float b1, float b2, float b3,
            float angle) {
            this.f0 = f0;
            this.f1 = f1;
            this.f2 = f2;
            this.f3 = f3;

            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;

            this.angle = angle;
        }

        public static Orientation Pointy = 
            new Orientation((float) Math.Sqrt(3.0), (float) (Math.Sqrt(3.0) / 2.0), 0.0f, (float) (3.0 / 2.0),
                (float) Math.Sqrt(3.0) / 3.0f, (float) -1.0 / 3.0f, 0.0f, 2.0f / 3.0f,
                0.5f);

        public static Orientation Flat =
            new Orientation(3.0f / 2.0f, 0.0f, (float) Math.Sqrt(3.0) / 2.0f, (float) Math.Sqrt(3.0),
                2.0f / 3.0f, 0.0f, -1.0f / 3.0f, (float) Math.Sqrt(3.0) / 3.0f,
                0.0f);


    }
}
