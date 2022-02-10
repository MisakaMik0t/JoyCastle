using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCB : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _easeAnimation;
    [SerializeField]
    private float _moveTime;
    [SerializeField]
    private bool _pingpong;

    private void Start()
    {
        StartCoroutine(Move(gameObject, Vector3.zero, Vector3.forward * 10, _moveTime, _pingpong));
    }

    private IEnumerator Move(GameObject target, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        var beginTime = Time.time;
        var startPosition = begin;
        var endPosition = end;
        do
        {
            while (Time.time - beginTime < time)
            {
                target.transform.position = Vector3.Lerp(startPosition, endPosition, _easeAnimation.Evaluate((Time.time - beginTime) / time));
                yield return null;
            }
            target.transform.position = endPosition;
            yield return null;
            var tmp = startPosition;
            startPosition = endPosition;
            endPosition = tmp;
            beginTime = Time.time;
        } while (pingpong);
    }

    private void Cut(int[,] a, int beginRow, int rowCount, int beginColumn, int columnCount, int targetBeginRow, int targetBeginColumn)
    {
        var tmp = new int[rowCount, columnCount];
        for (int r = 0; r < rowCount; ++r)
        {
            for (int c = 0; c < columnCount; ++c)
            {
                tmp[r, c] = a[beginRow + r, beginColumn + c];
            }
        }
        for (int r = 0; r < rowCount; ++r)
        {
            for (int c = 0; c < columnCount; ++c)
            {
                a[targetBeginRow + r, targetBeginColumn + c] = tmp[r, c];
            }
        }
    }

    private bool Check(string s, HashSet<string> set)
    {
        foreach (var element in set)
        {
            s = s.Replace(element, "");
        }
        return s == "";
    }

    private void Tutorial(){
        /*
         *  首先弹出游戏窗口解释规则
         *  然后选择一个空位让玩家填数字，如果数字不符合规则，这弹出不符合规则的解释，并高亮冲突的数字，直到玩家输入正确的数字为止
         *  可复用的地方就是弹窗，高亮界面，高亮可输入区域界面
         */
    }
}
