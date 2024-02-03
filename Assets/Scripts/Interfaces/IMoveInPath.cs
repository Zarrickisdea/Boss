public interface IMoveInPath
{
    void setPath(UnityEngine.Transform[] pathPoints);
    void setMoveSpeed(float moveSpeed);
    void setRotationTime(float rotationTime);
    void moveInPath();
}
