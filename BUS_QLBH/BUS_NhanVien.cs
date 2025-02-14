using DAL_QLBH;
using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLBH
{
   public class BUS_NhanVien
    {
        public bool NhanVienDangNhap(DTO_NhanVien nv)
        {
            return dalNhanVien.NhanVienDangNhap(nv);
        }


        public string encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder(); 
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        DAL_NhanVien dalNhanVien = new DAL_NhanVien();
        public DataTable getNhanVien()
        {
            return dalNhanVien.getNhanVien();
        }
        public bool insertNhanVien(DTO_NhanVien Nv)
        {
            return dalNhanVien.insertNhanVien(Nv);
        }
        public bool UpdateNhanVien(DTO_NhanVien Nv)
        {
            return dalNhanVien.UpdateNhanVien(Nv);
        }
        public bool DeleteNnhanVien(string tenDangNhap)
        {
            return dalNhanVien.DeleteNnhanVien(tenDangNhap);
        }
        public DataTable SearchNhanVien(string tenNhanvien)
        {
            return dalNhanVien.SearchNhanVien(tenNhanvien);
        }
        public DataTable VaiTroNhanVien(string email)
        {
            return dalNhanVien.VaiTroNhanVien(email);
        }
        public bool UpdateMatKhau(string email, string matKhauCu, string matKhauMoi)
        {
            return dalNhanVien.UpdateMatKhau(email, matKhauCu, matKhauMoi);
        }
        public bool NhanVienQuenMatKhau(string email)
        {
            return dalNhanVien.NhanVienQuenMatKhau(email);
        }
        public bool TaoMatKhau(string email, string matKhauMoi)
        {
            return dalNhanVien.TaoMatKhau(email, matKhauMoi);
        }
        public  DataTable TinhTrangNhanVien(string email)
        {
            return dalNhanVien.TinhTrangNhanVien(email);
        }

    }
}
