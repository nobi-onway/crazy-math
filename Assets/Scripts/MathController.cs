using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathController : MonoBehaviour
{
    #region UI
    [SerializeField] private TextMeshProUGUI _operationUI;
    #endregion

    private const int MAX_NUMBER = 100;
    private const int MIN_NUMBER = 1;

    private int _firstNumber;
    private int _secondNumber;
    private int _correctResult;
    private int _result;
    private int _operationIndex;
    private char[] _operations = {'+', '-', '*'};

    public void RandomOperation()
    {
        _operationIndex = Random.Range(0, _operations.Length);

        _firstNumber = RandomNumber();
        _secondNumber = RandomNumber();

        GenerateOperation();
    }

    public bool IsCorrectOperation()
    {
        return _correctResult == _result;
    }

    private void GenerateOperation()
    {
        switch(_operationIndex)
        {
            case 0:
                _correctResult = _firstNumber + _secondNumber;
                break;
            case 1:
                _correctResult = _firstNumber - _secondNumber;
                break;
            case 2:
                _correctResult = _firstNumber * _secondNumber;
                break;
        }

        _result = CanBeResult() ? _correctResult : RandomNumber();

        UpdateOperationUI();
    }

    private bool CanBeResult()
    {
        return Random.Range(MIN_NUMBER, MAX_NUMBER) % 2 == 0;
    }

    private int RandomNumber()
    {
        return Random.Range(MIN_NUMBER, MAX_NUMBER);
    }

    private void UpdateOperationUI()
    {
        _operationUI.text = $"{_firstNumber} {_operations[_operationIndex]} {_secondNumber} = {_result}";
    }
}
