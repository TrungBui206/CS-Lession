
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using StudentSubjectManagement.DAL;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.BUL
{
	public class LopBUL
	{
		private readonly LopDAL ld;
		private readonly SinhVienDAL svd;

		public LopBUL()
		{
			ld = new LopDAL();
			svd = new SinhVienDAL();
		}

		public List<LopHoc> GetAllLopHoc()
		{
			return ld.GetAllLopHoc();
		}

		public LopHoc? GetLopHocById(string maLop)
		{
			return ld.GetLopHocById(maLop.Trim());
		}

		public List<SinhVien> GetSinhVienByLop(string maLop)
		{
			return svd.GetSinhVienByLop(maLop);
		}

		public List<ValidationResult> ValidateLopHoc(LopHoc lh)
		{
			return lh.Validate();
		}

		public void AddLopHoc(LopHoc lh)
		{
			var errors = ValidateLopHoc(lh);

			if (ld.GetLopHocById(lh.MaLop) != null)
			{
				errors.Add(new ValidationResult(
					"Mã lớp đã tồn tại!",
					new[] { nameof(LopHoc.MaLop) }
				));
			}

			if (errors.Count > 0)
			{
				throw new ValidationException(
					string.Join("\n", errors.Select(e => e.ErrorMessage))
				);
			}

			lh.TenLop = lh.TenLop.Trim();
			ld.AddLopHoc(lh);
		}

		public void UpdateLopHoc(LopHoc lh)
		{
			var errors = ValidateLopHoc(lh);

			if (errors.Count > 0)
			{
				throw new ValidationException(
					string.Join("\n", errors.Select(e => e.ErrorMessage))
				);
			}

			if (ld.GetLopHocById(lh.MaLop) == null)
			{
				throw new InvalidOperationException(
					"Không tìm thấy lớp học cần sửa!"
				);
			}

			lh.TenLop = lh.TenLop.Trim();
			ld.UpdateLopHoc(lh);
		}

		public void DeleteLopHoc(string maLop)
		{
			if (string.IsNullOrWhiteSpace(maLop))
			{
				throw new InvalidOperationException(
					"Vui lòng nhập mã lớp cần xóa!"
				);
			}

			ld.DeleteLopHoc(maLop.Trim());
		}
	}
}
