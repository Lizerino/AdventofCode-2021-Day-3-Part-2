using System.Collections;

/*
 * This one is a mess.. couldnt get the logic right and a long day at work didnt exactly help.. ah well it works at least.. even if it aint pretty
 */

// Read input
string[] stringInput = File.ReadAllLines("Input.txt");

var lineLength = stringInput[0].Length;

// Turn into a list of bitarrays
List<BitArray> inputA = new();

foreach (var line in stringInput)
{
    BitArray tempBitArray = new BitArray(lineLength);
    for (int i = 0; i < lineLength; i++)
    {
        if (line[i] == '1')
        {
            tempBitArray.Set(i, true);
        }
    }
    inputA.Add(tempBitArray);
}

List<BitArray> inputB = new();

foreach (var line in stringInput)
{
    BitArray tempBitArray = new BitArray(lineLength);
    for (int i = 0; i < lineLength; i++)
    {
        if (line[i] == '1')
        {
            tempBitArray.Set(i, true);
        }
    }
    inputB.Add(tempBitArray);
}

// Get popular binary
for (int position = 0; position < lineLength; position++)
{
    var mostCommonBit = CalculateCommonBit(inputA,position);
    inputA = GetCommonBitRows(inputA,mostCommonBit, position);
    if (inputA.Count <= 1)
    {
        break;
    }
}

// Transfer and invert to oxygenBinary for later conversion to decimal
BitArray OxygenBinary = new BitArray(lineLength);
for (int i = 0; i < lineLength; i++)
{
    if (inputA[0][i] == true)
    {
        OxygenBinary.Set(lineLength - 1 - i, true);
    }
}

// Get unpopular binary
for (int position = 0; position < lineLength; position++)
{
    var mostCommonBit = CalculateCommonBit(inputB,position);
    if (mostCommonBit==true)
    {
        mostCommonBit = false;
    }
    else
    {
        mostCommonBit= true;
    }
    inputB = GetCommonBitRows(inputB,mostCommonBit, position);
    if (inputB.Count <= 1)
    {
        break;
    }
}

// Transfer and invert to oxygenBinary for later conversion to decimal
BitArray CO2Binary = new BitArray(lineLength);
for (int i = 0; i < lineLength; i++)
{
    if (inputB[0][i] == true)
    {
        CO2Binary.Set(lineLength - 1 - i, true);
    }
}

var result = new int[2];

OxygenBinary.CopyTo(result, 0);
CO2Binary.CopyTo(result, 1);

// print result
Console.WriteLine(result[0] * result[1]);

bool CalculateCommonBit(List<BitArray> input,int position)
{
    var counter = 0;
    foreach (var bitarray in input)
    {
        if (bitarray[position] == true)
        {
            counter++;
        }
    }
    if (counter >= input.Count - counter)
    {
        return true;
    }
    else
    {
        return false;
    }
}

List<BitArray> GetCommonBitRows(List<BitArray> input,bool mostCommonBit, int position)
{
    List<BitArray> tempList = new();
    foreach (var bitarray in input)
    {
        if (bitarray[position] == mostCommonBit)
        {
            tempList.Add(bitarray);
        }
    }
    return tempList;
}