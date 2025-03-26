using System.Collections.Generic;

public enum StatusType { Stun, Slow, KnockBack, Poison }

public abstract class Status
{
    public StatusType type;
    public float duration;
}

// 상태와 시간을 계산해서 추가와 삭제만 진행, 컨트롤러에서 중재만 발생
public class StatusHandler
{
    private ResourceHandler _resourceHandler;
    private List<Status> _currentStatuses = new();
    

    public void ConnectResource(ResourceHandler resourceHandler)
    {
        _resourceHandler = resourceHandler;
    }


    public void ApplyStatus(StatusType status, float duration)
    {
    }
}