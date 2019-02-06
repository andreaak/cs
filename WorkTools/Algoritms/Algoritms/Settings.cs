using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{
    class Settings_
    {
        static Dictionary<DataType, int> widthes;
        public static Dictionary<DataType, int> Widthes
        {
            get { return widthes; }
        }

        static Dictionary<DataType, int> heights;
        public static Dictionary<DataType, int> Heights
        {
            get { return Settings_.heights; }
        }

        static int spaceHorizontal;
        public static int SpaceHorizontal
        {
            get { return spaceHorizontal; }
        }

        static int spaceVertical;
        public static int SpaceVertical
        {
            get { return spaceVertical; }
        }

        static int baseFontHeight;
        public static int BaseFontHeight
        {
            get { return baseFontHeight; }
        }

        static int cycleAngle;
        public static int CycleAngle
        {
            get { return cycleAngle; }
        }

        static int methodBorderWidth;
        public static int MethodBorderWidth
        {
            get { return methodBorderWidth; }
        }

        static int connectorWidth;
        public static int ConnectorWidth
        {
            get { return Settings_.connectorWidth; }
        }

        static Settings_()
        {
            widthes = new Dictionary<DataType, int>();
            widthes.Add(DataType.method, 200);
            widthes.Add(DataType.property, 200);
            widthes.Add(DataType.branchCondition, 200);
            widthes.Add(DataType.branchElse, 200);
            widthes.Add(DataType.cycle, 200);
            heights = new Dictionary<DataType, int>();
            heights.Add(DataType.method, 100);
            heights.Add(DataType.property, 100);
            heights.Add(DataType.branchCondition, 100);
            heights.Add(DataType.branchElse, 100);
            heights.Add(DataType.cycle, 100);
            spaceHorizontal = 50;
            spaceVertical = 50;
            baseFontHeight = 8;
            cycleAngle = 30;
            methodBorderWidth = 5;
            connectorWidth = 5;
        }
    }
}
