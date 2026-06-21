using Svelto.ECS;

namespace ECS
{
    public static class Utils
    {
        public static bool IsValid(this EGID egid)
        {
            return egid.entityID != 0 && !egid.groupID.isInvalid;
        }
    }
}