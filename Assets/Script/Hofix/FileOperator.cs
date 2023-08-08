﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SGame
{
    // 自己定义的文件操作辅助类
    public class FileOperator
    {
        // 拷贝文件
        public static bool CopyFile(string src, string dst, bool isReplaceFile = true)
        {
            if (File.Exists(src))
            {
                string dir = Path.GetDirectoryName(dst);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                
                if (isReplaceFile == true)
                {
                    File.Copy(src, dst, true);
                }
                else
                {
                    if (File.Exists(dst) == false) // 不覆盖已经存在的文件
                    {
                        File.Copy(src, dst, false);
                    }
                }

                return true;
            }

            return false;
        }

        // 递归遍历复制文件
        public static bool CopyFiles(string src, string dst, bool isReplaceFile = true)
        {
            // 目标是一个文件
            if (File.Exists(src))
            {
                if (isReplaceFile == true)
                {
                    File.Copy(src, dst, true);
                }
                else
                {
                    if (File.Exists(dst) == false) // 不覆盖已经存在的文件
                    {
                        File.Copy(src, dst, false);
                    }
                }

                return true;
            }

            if (Directory.Exists(src))
            {
                if (Directory.Exists(dst) == false)
                {
                    // 目标目录不存在
                    Directory.CreateDirectory(dst);
                }

                // 遍历文件
                string[] files = Directory.GetFiles(src);
                foreach (string fileName in files)
                {
                    //string filePath = Path.Combine(src, fileName);
                    string name = Path.GetFileName(fileName);
                    string dstFilePath = Path.Combine(dst, name);
                    CopyFiles(fileName, dstFilePath, isReplaceFile);
                }

                // 遍历当前目录
                string[] subDirs = Directory.GetDirectories(src);
                foreach (string srcDir in subDirs)
                {
                    string dirName = Path.GetFileName(srcDir);
                    string dstDir = Path.Combine(dst, dirName);
                    CopyFiles(srcDir, dstDir, isReplaceFile);
                }

                return true;
            }

            throw new IOException("file not found!=" + src);
        }

        public delegate void ERR_CALLBACK(string err);

        static void MakeDirectorys(string filePath)
        {
            string dirName = System.IO.Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
        }
    }
}