Root Namespace: SkiDiveDev.DuoBitDataStructures

# DuoBitDataStructures
This is a project that supports bit array based data structures in C# (.NET Standard).
There are two unique aspects to this project:
1) It allows arbitrary sequences of bits to be stored together in a single array.
2) It enables indexing from both ends of the array ("left" and "right").

While its underlying data structure is a byte array, an arbitrary range of bits from one byte array can be stored into the DuoBitArray:
``` C#
var duoBitArrayUtilities = new DuoBitArrayUtilities();
var duoBitArray = DuoBitArray.Create(capacity: 24, duoBitArrayUtilities: duoBitArrayUtilities);

var sourceBits = new byte[] {0b0101_0011, 0b0010_0111};
duoBitArray.SetLeftBits(sourceBits, sourceArrayBitIndex: 3, destinationBitIndex: 29, numBits: 7);
```
This project's original purpose is to serve as the supporting data structure for a forthcoming data encoding project.
