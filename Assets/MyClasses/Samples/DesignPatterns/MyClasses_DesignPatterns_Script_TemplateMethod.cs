using UnityEngine;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_TemplateMethod : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Template Method - Abstract Class -----

        public abstract class DataMiner
        {
            public void Mine(string path)
            {
                OpenFile(path);
                ExtractData();
                ParseData();
                CloseFile();
            }

            public virtual void OpenFile(string path)
            {
                Debug.Log("Open " + path);
            }

            public abstract void ExtractData();
            public abstract void ParseData();

            public virtual void CloseFile()
            {
                Debug.Log("Close file");
            }
        } 

        #endregion

        #region ----- Template Method - Concrete Class -----

        class CSVDataMiner : DataMiner
        {
            public override void ExtractData()
            {
                Debug.Log("Extract CSV");
            }

            public override void ParseData()
            {
                Debug.Log("Parse CSV");
            }
        }   
        
        class PDFDataMiner : DataMiner
        {
            public override void ExtractData()
            {
                Debug.Log("Extract PDF");
            }

            public override void ParseData()
            {
                Debug.Log("Parse PDF");
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Template Method Pattern";
            _complexity = "★";
            _popularity = "★★";
            _defination = "Template Method là một mẫu Hành Vi cho phép xác định khung của"
                        + "\nthuật toán trong lớp cơ sở (base class) nhưng cho phép các"
                        + "\nlớp con (subclass) ghi đè các bước cụ thể mà không thay đổi"
                        + "\ncấu trúc tổng thể của thuật toán.";
            _structure = "- Abstract Class: là abstract class khai báo phương thức tổng"
                        + "\nthể và các bước, xác định bước nào là mặc định và bước nào"
                        + "\nlà trừu tượng (abstract)."
                        + "\n- Concrete Class: lớp ghi đè (override) lại các bước của"
                        + "\nAbstract Class.";
            _advantages = "- Tái sử dụng (reuse) mã bằng cách đưa các phần trùng lặp"
                        + "\n(duplicate) vào Abstract Class."
                        + "\n- Cho phép người dùng ghi đè chỉ một số phần nhất định"
                        + "\ngiúp chúng ít bị ảnh hưởng khi các phần khác thay đổi.";
            _disadvantages = "- Có khả năng vi phạm nguyên tắc Liskov Substitution (LSP)"
                            + "\nnếu Concrete Class thay đổi hành vi mong muốn của"
                            + "\nphương thức trừu tượng."
                            + "- Đôi khi Concrete Class bị giới hạn bởi khung thuật toán"
                            + "\ncủa Abstract Class."
                            + "\nCàng nhiều bước để ghi đè thì càng khó bảo trì";
        }

        protected override void _Usage()
        {
            Debug.Log("--------------------");
            PDFDataMiner pdfDataMiner = new PDFDataMiner();
            pdfDataMiner.Mine("pdf path");

            Debug.Log("--------------------");
            CSVDataMiner csvDataMiner = new CSVDataMiner();
            csvDataMiner.Mine("csv path");
        }

        #endregion
    }
}