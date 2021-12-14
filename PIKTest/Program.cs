using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace PIKTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = "C:\\Users\\sirba\\Desktop\\C#\\PIKTest\\csv";//здесь надо поменять путь, на папку с хранилищем csv
            List<int[]> finalMatrix = new List<int[]>();
            List<int[]> csvMatrix = new List<int[]>();
            List<int[]> csvNewMatrix = new List<int[]>();
            List<int[]> resultMatrix = new List<int[]>();
            resultMatrix.Add(new int[1] { 0 });
            string[] filesName;

            int lengthColumnCsvMatrix = 0;
            int lengthRowCsvMatrix = 0;
            int lengthColumnCsvNewMatrix = 0;
            int lengthRowCsvNewMatrix = 0;
            int lengthColumn;
            int lengthRow;
            
            filesName = Directory.GetFiles(dirName);
            for (int i = 0; i < filesName.Length; i++)
            {
                if (i == 1 || i == 0 || i % 2 == 1)
                {
                    csvMatrix = resultMatrix;
                    csvNewMatrix = parseCsvFile(filesName[i]);
                    castingArraysToOneImage(csvMatrix, csvNewMatrix);
                    resultMatrix = sumTwoCSV(csvMatrix, csvNewMatrix);
                } else
                {
                    csvMatrix = resultMatrix;
                    csvNewMatrix = parseCsvFile(filesName[i]);
                    castingArraysToOneImage(csvMatrix, csvNewMatrix);
                    resultMatrix = diffTwoCSV(csvMatrix, csvNewMatrix);
                }
            }
            
            string resName = dirName + "\\result.csv";
            finalMatrix = resultMatrix;
            try
            {
                string textCSV = "";
                for(int i = 0; i < finalMatrix.Count; i++)
                {
                    for (int j = 0; j < finalMatrix[i].Length; j++)
                    {
                        textCSV = textCSV + Convert.ToString(finalMatrix[i][j]) + "; ";
                    }
                    textCSV = textCSV + "\n";
                }

                File.WriteAllText(resName, textCSV);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            List<int[]> parseCsvFile(string path)
            {
                List<int[]> matrix = new List<int[]>();
                using (TextFieldParser tfp = new TextFieldParser(@path))
                {
                    tfp.TextFieldType = FieldType.Delimited;
                    tfp.SetDelimiters(";");

                    while (!tfp.EndOfData)
                    {
                        string[] fields = tfp.ReadFields();
                        int[] fieldsNum = new int[fields.Length];
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (fields[i] == "")
                            {
                                fieldsNum[i] = 0;
                            }
                            else if (fields[i] == "red")
                            {
                                fieldsNum[i] = 1;
                            }
                            else if (fields[i] == "green")
                            {
                                fieldsNum[i] = 5;
                            }
                            else if (fields[i] == "black")
                            {
                                fieldsNum[i] = 10;
                            }
                            else if (fields[i] == "white")
                            {
                                fieldsNum[i] = 12;
                            }
                            else if (fields[i] == "blue")
                            {
                                fieldsNum[i] = 15;
                            }
                        }
                        matrix.Add(fieldsNum);
                    }
                }
                return matrix;
            }
            void castingArraysToOneImage(List<int[]> csvMatrix1, List<int[]> csvNewMatrix1) {
                for (int i = 0; i < csvMatrix1.Count; i++)
                {
                    for (int j = 0; j < csvMatrix1[i].Length; j++)
                    {
                        lengthColumnCsvMatrix = j;
                    }
                    lengthRowCsvMatrix = i;
                }

                for (int i = 0; i < csvNewMatrix1.Count; i++)
                {
                    for (int j = 0; j < csvNewMatrix1[i].Length; j++)
                    {
                        lengthColumnCsvNewMatrix = j;
                    }
                    lengthRowCsvNewMatrix = i;
                }

                if (lengthColumnCsvMatrix >= lengthColumnCsvNewMatrix)
                {
                    lengthColumn = lengthColumnCsvMatrix + 1;
                }
                else
                {
                    lengthColumn = lengthColumnCsvNewMatrix + 1;
                }

                if (lengthRowCsvMatrix >= lengthRowCsvNewMatrix)
                {
                    lengthRow = lengthRowCsvMatrix + 1;
                }
                else
                {
                    lengthRow = lengthRowCsvNewMatrix + 1;
                }

                if (csvMatrix1.Count < lengthRow)
                {
                    int[] ps = new int[lengthColumn];
                    for (int i = 0; i < lengthColumn; i++)
                    {
                        ps[i] = 0;
                    }
                    int difference = lengthRow - csvMatrix1.Count;
                    for (int n = 0; n < difference; n++)
                    {
                        csvMatrix1.Add(ps);
                    }
                }

                if (csvNewMatrix1.Count < lengthRow)
                {
                    int[] ps = new int[lengthColumn];
                    for (int i = 0; i < lengthColumn; i++)
                    {
                        ps[i] = 0;
                    }
                    int difference = lengthRow - csvNewMatrix1.Count;
                    for (int n = 0; n < difference; n++)
                    {
                        csvNewMatrix1.Add(ps);
                    }
                }

                if (csvMatrix1[0].Length < lengthColumn)
                {
                    int diffrence = lengthColumn - csvMatrix1[0].Length;
                    for (int i = 0; i < lengthRow; i++)
                    {
                        int[] ps = new int[lengthColumn];
                        for (int j = 0; j < lengthColumn; j++)
                        {
                            if (j >= csvMatrix1[i].Length)
                            {
                                ps[j] = 0;
                                break;
                            }
                            ps[j] = csvMatrix1[i][j];
                        }
                        csvMatrix1[i] = ps;
                    }
                }

                if (csvNewMatrix1[0].Length < lengthColumn)
                {
                    int diffrence = lengthColumn - csvNewMatrix1[0].Length;
                    for (int i = 0; i < lengthRow; i++)
                    {
                        int[] ps = new int[lengthColumn];
                        for (int j = 0; j < lengthColumn; j++)
                        {
                            if (j >= csvNewMatrix1[i].Length)
                            {
                                ps[j] = 0;
                                break;
                            }
                            ps[j] = csvNewMatrix1[i][j];
                        }
                        csvNewMatrix1[i] = ps;
                    }
                }
            }
            List<int[]> sumTwoCSV(List<int[]> csvMatrix1, List<int[]> csvNewMatrix1)
            {
                List<int[]> betweenMatrix = new List<int[]>();
                for (int i = 0; i < lengthRow; i++)
                {
                    int[] ps = new int[lengthColumn];
                    for (int j = 0; j < lengthColumn; j++)
                    {
                        ps[j] = csvMatrix1[i][j] + csvNewMatrix1[i][j];
                    }
                    betweenMatrix.Add(ps);
                }
                return betweenMatrix;
            }
            List<int[]> diffTwoCSV(List<int[]> csvMatrix1, List<int[]> csvNewMatrix1)
            {
                List<int[]> betweenMatrix = new List<int[]>();
                for (int i = 0; i < lengthRow; i++)
                {
                    int[] ps = new int[lengthColumn];
                    for (int j = 0; j < lengthColumn; j++)
                    {
                        ps[j] = csvMatrix1[i][j] - csvNewMatrix1[i][j];
                    }
                    betweenMatrix.Add(ps);
                }
                return betweenMatrix;
            }
        }
    }
}
