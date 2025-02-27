﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLBH
{
   public class DTO_Khach
    {
        private string makhach;
        private string soDienThoai;
        private string tenKhach;
        private string diaChi;
        private string phai;
        private string emailKhach;
        private string Manv;

        public string Makhach
        {
            get
            {
                return makhach;
            }
            set
            {
                makhach = value;
            }
        }

        public string SoDienThoai
        {
            get
            {
                return soDienThoai;
            }
            set
            {
                soDienThoai = value;
            }
        }
        public string TenKhach
        {
            get
            {
                return tenKhach;
            }
            set
            {
                tenKhach = value;
            }
        }
        public string DiaChi
        {
            get
            {
                return diaChi;
            }
            set
            {
                diaChi = value;
            }
        }
        public string Phai
        {
            get
            {
                return phai;
            }
            set
            {
                phai = value;
            }
        }
        public string EmailKhach
        {
            get
            {
                return emailKhach;
            }
            set
            {
                emailKhach = value;
            }
        }
        private string GenerateRandomCode(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Constructor cập nhật


             public DTO_Khach(string makhach, string dienThoai, string tenKhach, string diaChi, string phai, string email = null)
        {
            this.makhach = makhach;
            this.soDienThoai = dienThoai;
            this.tenKhach = tenKhach;
            this.diaChi = diaChi;
            this.phai = phai;
            this.emailKhach = email;
        }

        // Constructor cho Insert (Tạo mã khách tự động)
        public DTO_Khach(string dienThoai, string tenKhach, string diaChi, string phai, string email)
        {
            this.makhach = GenerateRandomCode();
            this.soDienThoai = dienThoai;
            this.tenKhach = tenKhach;
            this.diaChi = diaChi;
            this.phai = phai;
            this.emailKhach = email;
        }

        // Constructor cho Delete
        public DTO_Khach(string makhach)
        {
            this.makhach = makhach;
        }


    }
}
