using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Threading;

Console.WriteLine("Counting Sort: ");

int[] arr = InputArr();

var sortedArray = CountSort(arr, true);

PrintArr(sortedArray);

Console.WriteLine("Radix sort ");
//int[] arrRadix = { 110, 1, 21, 53, 8, 98, 26, 163, 38, 897, 23, 23, 6, 2 };
int[] arrRadix = InputArr();
RadixSort(arrRadix);

PrintArr(sortedArray);

int[] CountSort(int[] arr, bool ascending)
{
    var maxValue = GetMaxValue(arr);
    var statsArr = new int[maxValue + 1];


    // Отримуєм елемент з основного масиву та записуємо його в statArr
    for (int i = 0; i < arr.Length; i++)
    {
        statsArr[arr[i]] += 1;
    }
    if (ascending)
    {
        for (int i = 0, j = 0; i < statsArr.Length; i++)
        {
            while (statsArr[i] > 0)
            {
                arr[j] = i;
                j++;
                statsArr[i]--;
            }
        }
    }
    else
    {
        for (int i = statsArr.Length - 1, j = 0; i >= 0; i--)
        {
            // SortArr починаємо дивитися з ласт індексу і добавляти в початок основого масиву
            while (statsArr[i] > 0)
            {
                arr[j] = i;
                j++;
                statsArr[i]--;
            }
        }
    }


    return arr;
}

void RadixSort(int[] arr)
{
    int maxVal = GetMaxValue(arr);

    for (int exp = 1; maxVal / exp > 0; exp *= 10)
        СountSortRadix(arr, exp);
}

void СountSortRadix(int[] arr, int exp)
{
    int size = arr.Length;
    int[] outputArr = new int[size];

    // корзинка для кожної цифри від 0-9
    int[] binArr = new int[10];

    // записуємо цифри по корзинкам
    for (int i = 0; i < size; i++)
        binArr[(arr[i] / exp) % 10]++;

    for (int i = 1; i < 10; i++)
        binArr[i] += binArr[i - 1];

    // будуємо outputArr 
    for (int i = 0; i <= size-1; i++)
    {
        outputArr[binArr[(arr[i] / exp) % 10] - 1] = arr[i];
        binArr[(arr[i] / exp) % 10]--;
    }

    for (int i = 0; i < size; i++)
        arr[i] = outputArr[i];
}

int GetMaxValue(int[] arr)
{
    var maxVal = arr[0];

    for (int i = 0; i < arr.Length; i++)
    {
        if (arr[i] > maxVal)
        {
            maxVal = arr[i];
        }
    }

    return maxVal;
}

// Це просто для вводу масива, з усіма перевірками
int[] InputArr()
{
    int size = SizeInput();
    int[] arr = new int[size];

    InputElements(ref arr);

    return arr;
}

int SizeInput()
{
    Console.WriteLine("Hello input size of your arr: ");

    if (!(int.TryParse(Console.ReadLine(), out int size) && size > 0))
    {
        Console.WriteLine("Please input unsigned integer size!");
        Console.WriteLine("Restarting...");
        Thread.Sleep(200);
        size = 0;
        return size + SizeInput();
    }
    else
    {
        return size;
    }
}

void InputElements(ref int[] arr)
{
    Console.WriteLine("Now input unsgined integer numbers: ");
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = InputElement();
    }
}

int InputElement()
{
    if (!(int.TryParse(Console.ReadLine(), out int element) && element > -1))
    {
        Console.WriteLine("Inccorect input, please input only usigned integer value!");
        Console.WriteLine("Restarting...");
        Thread.Sleep(200);
        element = 0;
        return element + InputElement();
    }

    return element;
}

void PrintArr(int[] arr)
{
    foreach (var item in arr)
    {
        Console.Write(item + " ");
    }
    Console.WriteLine();
}