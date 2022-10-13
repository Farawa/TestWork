using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IListener
{
    public static int currentPlace { get; private set; } = 0;
    private float verticalOffset;
    private Vector3 mouseDownPosition;
    private bool isMoving = false;
    private bool isAlive = false;

    private void Awake()
    {
        MessagesManager.Follow(this);
    }

    private void OnDestroy()
    {
        MessagesManager.UnFollow(this);
    }

    private void Start()
    {
        verticalOffset = transform.localScale.y / 2 + DataManager.DataSO.stepSize.y / 2;
    }

    private void PlaceOnStart()
    {
        StopAllCoroutines();
        isMoving = false;
        isAlive = true;
        currentPlace = 0;
        var position = GetTargetPosition();
        position.x = 0;
        transform.position = position;
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
        }
        if (!Input.GetMouseButtonUp(0) || isMoving)
        {
            return;
        }
        if (Mathf.Abs(mouseDownPosition.x - Input.mousePosition.x) > DataManager.DataSO.magnitudeForSwipe)
        {
            if (mouseDownPosition.x + DataManager.DataSO.magnitudeForSwipe > Input.mousePosition.x)
            {
                Move(Direction.left);
            }
            if (mouseDownPosition.x - DataManager.DataSO.magnitudeForSwipe < Input.mousePosition.x)
            {
                Move(Direction.right);
            }
            return;
        }
        else
        {
            StartCoroutine(JumpCoroutine());
        }
    }

    private void Move(Direction direction)
    {
        if ((transform.position.x - DataManager.DataSO.moveDistance < -DataManager.DataSO.stepSize.x / 2 && direction == Direction.left) ||
            (transform.position.x + DataManager.DataSO.moveDistance > DataManager.DataSO.stepSize.x / 2 && direction == Direction.right))
        {
            return;
        }
        if (direction == Direction.left)
        {
            StartCoroutine(MoveCoroutine(transform.position + (Vector3.left * DataManager.DataSO.moveDistance)));
        }
        else
        {
            StartCoroutine(MoveCoroutine(transform.position + (Vector3.right * DataManager.DataSO.moveDistance)));
        }
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        isMoving = true;
        var startPlace = transform.position;
        var iterations = 50 * DataManager.DataSO.moveDuration;
        for (int i = 0; i < iterations; i++)
        {
            var currentIteration = i / iterations;
            var position = currentIteration * (targetPosition - startPlace) + startPlace;
            position.y += DataManager.DataSO.moveHeight * DataManager.DataSO.moveCurve.Evaluate(currentIteration);
            transform.position = position;

            yield return new WaitForFixedUpdate();
        }
        transform.position = targetPosition;
        isMoving = false;
    }

    private IEnumerator JumpCoroutine()
    {
        isMoving = true;
        currentPlace++;
        var startPlace = transform.position;
        var finishPlace = GetTargetPosition();
        var iterations = 50 * DataManager.DataSO.jumpDuration;
        for (int i = 0; i < iterations; i++)
        {
            var currentIteration = i / iterations;
            var position = currentIteration * (finishPlace - startPlace) + startPlace;
            position.y += DataManager.DataSO.jumpHeight * DataManager.DataSO.jumpCurve.Evaluate(currentIteration);
            transform.position = position;

            yield return new WaitForFixedUpdate();
        }
        transform.position = finishPlace;
        isMoving = false;
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(transform.position.x, DataManager.DataSO.stepSize.y * currentPlace + verticalOffset, currentPlace * DataManager.DataSO.stepSize.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            MessagesManager.SendMessage(new EndMessage(currentPlace));
        }
    }

    public void GetMessage(Message message)
    {
        if (message is StopMessage)
        {
            StopAllCoroutines();
            isAlive = false;
        }
        if (message is RestartMessage)
        {
            PlaceOnStart();
        }
    }
}
