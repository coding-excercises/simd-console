# simd-console
A sample console program in C# using SIMD vectors. 

The goal of this program is to compare the difference in elapsed 
time for array multiplication between two approaches - with and without using SIMD (Single Instruction Multiple Data) instructions.


This program has been tested using the following hardware and software -
- i7-7700HQ, 8 Cores @ 2.80Ghz
- 16 GB RAM
- Windows 10 Professional
- Visual Studio 2019 Community Edition
- .NET Framework v4.7.2


Sample program test runs produced the following results -

|  Array Size   |   Without SIMD   |    With SIMD      |
| ------------- | ---------------- | ----------------- |
| 100,000       | 00:00:00.0005520 | 00:00:00.0012228  | 
| 1,000,000     | 00:00:00.0069719 | 00:00:00.0032409  | 
| 10,000,000    | 00:00:00.0593490 | 00:00:00.0298787  | 
| 100,000,000   | 00:00:00.4801113 | 00:00:00.2604438  | 
| 1,000,000,000 | 00:04:50.5606415 | 00:00:46.8211629  | 
