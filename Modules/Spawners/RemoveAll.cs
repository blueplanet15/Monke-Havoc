namespace MonkeHavoc.Modules.Spawners
{
    public class RemoveAll
    {
        public static void ExecuteAllOfThem()
        {
            CubeSpammer.RemoveCubes();
            CubeSpawner.RemoveCubes();
            CylinderSpammer.RemoveCylinders();
            CylinderSpawner.RemoveCylinders();
            SphereSpammer.RemoveSpheres();
            SphereSpawner.RemoveSpheres();
        }
    }
}