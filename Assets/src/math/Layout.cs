using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.math {
    struct Layout {
        public Orientation or;
        public Vector2 size;
        public Vector2 origin;
        public Layout(Orientation or, Vector2 size, Vector2 origin) {
            this.or = or;
            this.size = size;
            this.origin = origin;
        }

        public Vector2 HexPixel(HexLoc h) {
            float x = (or.f0 * h.q + or.f1 * h.r) * size.x;
            float y = (or.f2 * h.q + or.f3 * h.r) * size.y;
            return new Vector2((float) (x + origin.x), (float) (y + origin.y));
        }

        public HexLoc PixelHex(Vector2 v) {
            Vector2 v2 = new Vector2((v.x - origin.x) / size.x,
                     (v.y - origin.y) / size.y);
            float q = or.b0 * v2.x + or.b1 * v2.y;
            float r = or.b2 * v2.x + or.b3 * v2.y;
            return new FractionalHex(q, r, -q - r).Round();
        }

        public Vector2 CornerOffset(int corner) {
            double angle = 2.0 * Math.PI *
                     (corner + or.angle) / 6;
            return new Vector2((float) (size.x * Math.Cos(angle)), (float) (size.y * Math.Sin(angle)));
        }

        public Vector2[] PolygonCorners(HexLoc h) {
            Vector2[] corners = new Vector2[6];
            Vector2 center = HexPixel(h);
            for (int i = 0; i < 6; i++) {
                Vector2 offset = CornerOffset(i);
                corners[i] = center + offset;
            }
            return corners;
        }
    }

    struct FractionalHex {
        float q, r, s;
        public FractionalHex(float q, float r, float s) {
            this.q = q;
            this.r = r;
            this.s = s;
        }

        public HexLoc Round() {
            int _q = (int) Math.Round(q);
            int _r = (int) Math.Round(r);
            int _s = (int) Math.Round(s);
            double q_diff = Math.Abs(_q - q);
            double r_diff = Math.Abs(_r - r);
            double s_diff = Math.Abs(_s - s);
            if (q_diff > r_diff && q_diff > s_diff) {
                _q = -_r - _s;
            } else if (r_diff > s_diff) {
                _r = -_q - _s;
            } else {
                _s = -_q - _r;
            }
            return new HexLoc(_q, _r, _s);
        }
    }
}
