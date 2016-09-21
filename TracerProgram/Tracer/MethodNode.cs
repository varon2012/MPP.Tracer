namespace Tracer
{
    internal class MethodNode
    {
        public MethodInfo Info { get; }
        public bool isWorking { get; set; }
        public int Heignt { get; }

        public MethodNode(MethodInfo info, int height)
        {
            Info = info;
            Heignt = height;
            isWorking = true;
        }

    }
}
