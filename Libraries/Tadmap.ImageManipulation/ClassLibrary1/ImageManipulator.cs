using System.Drawing;
using System;
using System.Collections.Generic;
namespace ImageManipulation
{
   public class ImageManipulator
   {
      static public Bitmap ResizeAndCrop(Bitmap oSource, int iTargetWidth, int iTargetHeight)
      {
         Bitmap oResult = new Bitmap(iTargetWidth, iTargetHeight);

         float fScale;
         float fXOffset = 0;
         float fYOffset = 0;

         if (iTargetWidth / iTargetHeight > oSource.Width / oSource.Height)
         {
            fScale = ((float)iTargetWidth) / (float)oSource.Width;
            fYOffset = -((oSource.Height * fScale) - iTargetHeight) / 2;
         }
         else
         {
            fScale = ((float)iTargetHeight) / (float)oSource.Height;
            fXOffset = -((oSource.Width * fScale) - iTargetWidth) / 2;
         }

         using (Graphics g = Graphics.FromImage((Image)oResult))
         {
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(oSource, fXOffset, fYOffset, oSource.Width * fScale, oSource.Height * fScale);
         }

         return oResult;
      }

      static public Bitmap FitToRectangle(Bitmap oSource, int iTargetWidth, int iTargetHeight)
      {
         float fScale;
         int iResultWidth = 0;
         int iResultHeight = 0;

         if (iTargetWidth / iTargetHeight > oSource.Width / oSource.Height)
         {
            fScale = ((float)iTargetHeight) / (float)oSource.Height;
            iResultHeight = iTargetHeight;
            iResultWidth = (int)(oSource.Width * fScale);
         }
         else
         {
            fScale = ((float)iTargetWidth) / (float)oSource.Width;
            iResultWidth = iTargetWidth;
            iResultHeight = (int)(oSource.Height * fScale);
         }

         Bitmap oResult = new Bitmap(iResultWidth, iResultHeight);

         using (Graphics g = Graphics.FromImage((Image)oResult))
         {
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(oSource, 0, 0, oSource.Width * fScale, oSource.Height * fScale);
         }

         return oResult;
      }

      static public Bitmap Resize(Bitmap oSource, int iTargetWidth, int iTargetHeight)
      {
         float fXScale;
         float fYScale;

         fXScale = ((float)iTargetHeight) / (float)oSource.Height;
         fYScale = ((float)iTargetWidth) / (float)oSource.Width;

         Bitmap oResult = new Bitmap(iTargetWidth, iTargetHeight);

         using (Graphics g = Graphics.FromImage((Image)oResult))
         {
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(oSource, 0, 0, oSource.Width * fXScale, oSource.Height * fYScale);
         }

         return oResult;
      }

      static public List<List<Bitmap>> CreateTiles(Bitmap oSource, int iSize)
      {
         List<List<Bitmap>> oTiles = new List<List<Bitmap>>();

         int iStartY = 0;

         do
         {
            int iStartX = 0;
            List<Bitmap> oRow = new List<Bitmap>();

            do
            {
               Bitmap oResult = new Bitmap(iSize, iSize);
               using (Graphics g = Graphics.FromImage((Image)oResult))
               {
                  g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                  g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                  g.DrawImage(oSource, -iStartX, -iStartY, oSource.Width, oSource.Height);
               }
               oRow.Add(oResult);

               iStartX += iSize;
            }
            while (iStartX < oSource.Width);

            oTiles.Add(oRow);

            iStartY += iSize;
         }
         while (iStartY < oSource.Height);

         return oTiles;
      }

      static public Bitmap CenterImage(Bitmap oSource, int iWidth, int iHeight)
      {
         Bitmap oResult = new Bitmap(iWidth, iHeight);

         using (Graphics g = Graphics.FromImage((Image)oResult))
         {
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, iWidth, iHeight));
            g.DrawImage(
                oSource,
                iWidth / 2 - oSource.Width / 2,
                iHeight / 2 - oSource.Height / 2,
                oSource.Width,
                oSource.Height
            );
         }

         return oResult;
      }
   }
}
