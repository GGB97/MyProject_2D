public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput(); // 입력처리를 할 떄
    public void Update();
    public void PhysicsUpdate(); // 물리적 업데이트

}