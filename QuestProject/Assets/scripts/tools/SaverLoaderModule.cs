using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tools
{
    /// <summary>
    /// Universal module for saving and loading any text information
    /// </summary>
    public static class SaverLoaderModule
    {
        #region for window saves in application folder

        //uncomment line below
        public static string StandartSavesPath => Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "/Standart_Saves";

        #endregion for window saves in application folder

        #region for universal saves

        //public static string StandartSavesPath => Application.persistentDataPath + "/Standart_Saves";

        #endregion for universal saves

        #region public functions

        /// <summary>
        /// Saves data to a file with the specified name. Remember that this function saves the file in a standard folder.
        /// </summary>
        /// <param name="FileName">Name of file</param>
        /// <param name="StringData">Your string data</param>
        public static void SaveMyDataToFile(string FileName, string StringData)
        {
            try
            {
                FileName = Normalizer(FileName);
                OverGeneratePath(StandartSavesPath + FileName);
                FileStream fileStream = new FileStream(StandartSavesPath + FileName, FileMode.Create);
                StreamWriter MyFile = new StreamWriter(fileStream);
                MyFile.Write(StringData);
                MyFile.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        /// <summary>
        /// Load string data from file. Remember that this function loads the file from the standard folder.
        /// </summary>
        /// <param name="FileName">Name of file</param>
        /// <returns>String data or "", if file don`t exist</returns>
        public static string LoadMyDataFromFile(string FileName)
        {
            try
            {
                FileName = Normalizer(FileName);
                if (File.Exists(StandartSavesPath + FileName))
                {
                    StreamReader MyFile = new StreamReader(StandartSavesPath + FileName);
                    string DString = MyFile.ReadToEnd();
                    MyFile.Close();
                    return DString;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            return "";
        }

        /// <summary>
        /// Saves data to a file with the specified name. Remember that in this function you must specify the full path to the file.
        /// </summary>
        /// <param name="FullPath">Full path to file</param>
        /// <param name="StringData">Your string data</param>
        public static void SaveMyDataTo(string FullPath, string StringData)
        {
            try
            {
                OverGeneratePath(FullPath);
                FileStream fileStream = new FileStream(FullPath, FileMode.Create);
                StreamWriter MyFile = new StreamWriter(fileStream);
                MyFile.Write(StringData);
                MyFile.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        /// <summary>
        /// Load string data from file. Remember that in this function you must specify the full path to the file.
        /// </summary>
        /// <param name="FullPath">Full path to file</param>
        /// <returns>String data or "", if file don`t exist</returns>
        public static string LoadMyDataFrom(string FullPath)
        {
            try
            {
                if (File.Exists(FullPath))
                {
                    StreamReader MyFile = new StreamReader(FullPath);
                    string DString = MyFile.ReadToEnd();
                    MyFile.Close();
                    return DString;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            return "";
        }

        #endregion public functions

        #region private functions

        private static string Normalizer(string fileName)
        {
            if (fileName.IndexOf('/') == 0)
            {
                return fileName;
            }
            else
            {
                return "/" + fileName;
            }
        }

        private static string OverGeneratePath(string fileName)
        {
            string[] tempArray = fileName.Split('/');
            List<string> finalListOfPathParts = new List<string>();

            #region delete extra "/"

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (tempArray[i] != "")
                {
                    finalListOfPathParts.Add(tempArray[i]);
                }
            }

            #endregion delete extra "/"

            #region create returned string and overGenerating path

            string returnedPath = "";
            for (int i = 0; i < finalListOfPathParts.Count - 1; i++)
            {
                returnedPath += finalListOfPathParts[i] + "/";
                if (!Directory.Exists(returnedPath))
                {
                    try
                    {
                        Directory.CreateDirectory(returnedPath);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }
            returnedPath += finalListOfPathParts[finalListOfPathParts.Count - 1];

            #endregion create returned string and overGenerating path

            return returnedPath;
        }

        #endregion private functions
    }
}