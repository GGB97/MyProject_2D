public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput(); // �Է�ó���� �� ��
    public void Update();
    public void PhysicsUpdate(); // ������ ������Ʈ

}