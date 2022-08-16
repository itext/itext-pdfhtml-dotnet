using System;
using System.Collections.Generic;
using System.Linq;
using iText.Commons.Utils;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace iText.Html2pdf.Utils {
    public class ImageSizeMeasuringListener : IEventListener {
        internal Rectangle cropbox;

        public Rectangle bbox;

        internal int page;

        public ImageSizeMeasuringListener(int page) {
            if (cropbox == null) {
                cropbox = new Rectangle(float.Epsilon, float.Epsilon, float.MaxValue, float.MaxValue);
            }
            this.page = page;
        }

        public virtual void EventOccurred(IEventData data, EventType type) {
            switch (type) {
                case EventType.RENDER_IMAGE: {
                    ImageRenderInfo renderInfo = (ImageRenderInfo)data;
                    Matrix imageCtm = renderInfo.GetImageCtm();
                    bbox = CalcImageRect(imageCtm);
                    goto default;
                }

                default: {
                    break;
                }
            }
        }

        public virtual ICollection<EventType> GetSupportedEvents() {
            return null;
        }

        private double GetWidth(Matrix m) {
            return Math.Sqrt(Math.Pow(m.Get(Matrix.I11), 2) + Math.Pow(m.Get(Matrix.I21), 2));
        }

        private double GetHeight(Matrix m) {
            return Math.Sqrt(Math.Pow(m.Get(Matrix.I12), 2) + Math.Pow(m.Get(Matrix.I22), 2));
        }

        private Rectangle CalcImageRect(Matrix ctm) {
            if (ctm == null) {
                return null;
            }
            Point[] points = TransformPoints(ctm, false, new Point(0, 0), new Point(0, 1), new Point(1, 0), new Point(
                1, 1));
            return GetAsRectangle(points[0], points[1], points[2], points[3]);
        }

        private Rectangle GetAsRectangle(Point p1, Point p2, Point p3, Point p4) {
            IList<double> xs = JavaUtil.ArraysAsList(p1.GetX(), p2.GetX(), p3.GetX(), p4.GetX());
            IList<double> ys = JavaUtil.ArraysAsList(p1.GetY(), p2.GetY(), p3.GetY(), p4.GetY());
            double left = Enumerable.Min(xs);
            double bottom = Enumerable.Min(ys);
            double right = Enumerable.Max(xs);
            double top = Enumerable.Max(ys);
            return new Rectangle((float)left, (float)bottom, (float)right, (float)top);
        }

        private Point[] TransformPoints(Matrix transformationMatrix, bool inverse, params Point[] points) {
            AffineTransform t = new AffineTransform(transformationMatrix.Get(Matrix.I11), transformationMatrix.Get(Matrix
                .I12), transformationMatrix.Get(Matrix.I21), transformationMatrix.Get(Matrix.I22), transformationMatrix
                .Get(Matrix.I31), transformationMatrix.Get(Matrix.I32));
            Point[] transformed = new Point[points.Length];
            if (inverse) {
                try {
                    t = t.CreateInverse();
                }
                catch (NoninvertibleTransformException e) {
                    throw new Exception("Exception during effective image rectangle calculation", e);
                }
            }
            t.Transform(points, 0, transformed, 0, points.Length);
            return transformed;
        }
    }
}
