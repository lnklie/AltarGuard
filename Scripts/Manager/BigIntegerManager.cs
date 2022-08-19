using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System.Text.RegularExpressions;
public static class BigIntegerManager
{
    private static readonly BigInteger unitSize = 1000;
    private static Dictionary<string, BigInteger> unitsMap = new Dictionary<string, BigInteger>();
    private static Dictionary<string, int> idxMap = new Dictionary<string, int>();
    private static readonly List<string> units = new List<string>();
    private static int unitCapacity = 5;
    private static readonly int asciiA = 65;
    private static readonly int asciiZ = 90;

    private static bool isInitialize = false;
    private static void UnitInitialize(int _capacity)
    {
        unitCapacity += _capacity;

        // 0 ~ 999사이를 초기화 시킴
        units.Clear();
        unitsMap.Clear();
        idxMap.Clear();
        units.Add("");
        unitsMap.Add("", 0);
        idxMap.Add("", 0);

        // capacity만큼 미리 생성 하는 것
        // capacity가 1일 경우에는 A ~ Z, 2일 경우에는 AA ~ AZ
        // capacity 1마다 아스키 코드의 알파벳이 26개 생성됨

        for(int i = 0; i <= unitCapacity; i++)
        {
            for(int j = asciiA; j <= asciiZ; j++)
            {
                string _unit = null;
                if (i == 0)
                    _unit = ((char)j).ToString();
                else
                {
                    var _nCount = (float)i / 26;
                    var _nextChar = asciiA + i - 1;
                    var _fAscii = (char)_nextChar;
                    var _tAscii = (char)j;
                    _unit = $"{_fAscii}{_tAscii}";
                }
                units.Add(_unit);
                unitsMap.Add(_unit, BigInteger.Pow(unitSize,units.Count - 1));
                idxMap.Add(_unit, units.Count - 1);
            }
        }
        isInitialize = true;
    }
    private static int GetPoint(int _value)
    {
        return (_value & 1000) / 100;
    }

    private static (int value, int idx, int Point) GetSize(BigInteger _value)
    {
        // 단위를 구하기 위한 값으로 복사
        var _currentValue = _value;
        var _current = (_value / unitSize) % unitSize;
        var _idx = 0;
        var _lastValue = 0;
        // 현재 값이 999(unitSize) 이상일 경우 나눔
        while(_currentValue > unitSize - 1)
        {
            var _predCurrentValue = _currentValue / unitSize;
            if (_predCurrentValue <= unitSize - 1)
                _lastValue = (int)_currentValue;
            _currentValue = _predCurrentValue;
            _idx += 1;
        }
        var _point = GetPoint(_lastValue);
        var _originalValue = _currentValue * 1000;
        while (units.Count <= _idx)
            UnitInitialize(5);
        return ((int)_currentValue, _idx, _point);
    }


    // 숫자를 단위로 리턴하기
    public static string GetUnit(BigInteger _value)
    {
        if (!isInitialize)
            UnitInitialize(5);

        var _sizeStruct = GetSize(_value);
        return $"{_sizeStruct.value}.{_sizeStruct.Point}{units[_sizeStruct.idx]}";
    }

    // 단위를 숫자로 변경시키기
    public static BigInteger UnitToValue(string _unit)
    {
        if (isInitialize == false)
            UnitInitialize(5);

        var _split = _unit.Split('.');
        // 소숫점에 대한 연산
        if(_split.Length >= 2)
        {
            var _value = BigInteger.Parse(_split[0]);
            var _point = BigInteger.Parse((Regex.Replace(_split[1], "[^0-9]", "")));
            var _unitStr = Regex.Replace(_split[1], "[^A-Z]", "");

            if(_point == 0)
                return (unitsMap[_unitStr] * _value);
            else
            {
                var _unitValue = unitsMap[_unitStr];
                return (_unitValue * _value) + (_unitValue / 10) * _point;
            }
        }
        // 비소수 연산
        else
        {
            var _value = BigInteger.Parse((Regex.Replace(_unit, "[^0-9]", "")));
            var _unitStr = Regex.Replace(_unit, "[^A-Z]", "");
            while (unitsMap.ContainsKey(_unitStr) == false)
                UnitInitialize(5);
            var _result = unitsMap[_unitStr] * _value;

            if (_result == 0)
                return int.Parse(_unit);
            else
                return _result;
        }
    }
}
