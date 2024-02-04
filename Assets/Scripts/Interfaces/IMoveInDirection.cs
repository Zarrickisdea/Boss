using UnityEngine;
public interface IMoveInDirection
{
    void setDirection(Vector3 direction);
    void moveInDirection(Vector3 targetPosition);
    void stateOn(float seconds);
    void stateOff(float seconds);
}
