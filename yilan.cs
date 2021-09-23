using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

namespace YILANOYUNU
{
    class yilan
    {

        yilan_parcaları[] yılan_parca;
        int yilan_buyuklugu;
        yon yonumuz;
        public yilan()
        {
            yılan_parca = new yilan_parcaları[3];
            yilan_buyuklugu = 3;
            yılan_parca[0] = new yilan_parcaları(150, 150);
            yılan_parca[1] = new yilan_parcaları(160, 150);
            yılan_parca[2] = new yilan_parcaları(170, 150);
        }
        public void ilerle(yon yon)
        {
            yonumuz = yon;
            if (yon._x == 0 && yon._y == 0)
            {

            }
            else
            {

                for (int i = yılan_parca.Length - 1; i > 0; i--)
                {
                    yılan_parca[i] = new yilan_parcaları(yılan_parca[i - 1].x_, yılan_parca[i - 1].y_); //her parçanın bir öncekinin yerine geçmesini sağlıyoruz
                }
                yılan_parca[0] = new yilan_parcaları(yılan_parca[0].x_ + yon._x, yılan_parca[0].y_ + yon._y);

            }
        }
        public void buyu()
        {
            Array.Resize(ref yılan_parca, yılan_parca.Length + 1);
            yılan_parca[yılan_parca.Length - 1] = new yilan_parcaları(yılan_parca[yılan_parca.Length - 2].x_ - yonumuz._x, yılan_parca[yılan_parca.Length - 2].y_ - yonumuz._y);
            yilan_buyuklugu++;
        }
        public Point GetPos(int number)
        {
            return new Point(yılan_parca[number].x_, yılan_parca[number].y_);
        }
        public int Yılan_buyukluğu
        {
            get
            {
                return yilan_buyuklugu;
            }
        }
    }
    class yilan_parcaları
    {
        public int x_; 
        public int y_;
        public readonly int size_x; //readonly olduğu için bunu daha sonra değiştiremiyoruz
        public readonly int size_y;
        public yilan_parcaları(int x,int y)
        {
            x_ = x; //konum
            y_ = y; //konum
            size_x = 10;
            size_y = 10;
        }
    }
    class yon
    {
        public readonly int _x;
        public readonly int _y;
        public yon(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
