using ProyectoPrueba.DTOs;
using ProyectoPrueba.Interfaces;
using ProyectoPrueba.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoPrueba.Services
{
    public class UserRepository
        : IUserRepository, IDisposable
    {
        private BaseContext _baseContext;
        private bool disposed = false;

        public UserRepository(BaseContext baseContext)
        {
            _baseContext = baseContext;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._baseContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void insertUser(User user)
        {
            if (user.Email.Trim().Length <= 0) throw new Exception("No se indicó el mail");

            if (user.Password.Trim().Length <= 0) throw new Exception("No se indicó la clave");

            if (user.OrganizationId <= 0) throw new Exception("No se indicó la organización");

            user.Password = GetSHA256(user.Password);

            this._baseContext.Users.Add(user);

            this._baseContext.SaveChanges();
        }
        public int ValidateUser(LoginDTO login)
        {
            if (login.Email.Trim().Length <= 0) throw new Exception("No se indicó el mail");

            if (login.Password.Trim().Length <= 0) throw new Exception("No se indicó la clave");

            var user = this._baseContext.Users.Where(c => c.Email == login.Email).FirstOrDefault();

            if( user == null) throw new Exception("El usuario no existe");

            if( user.Password == GetSHA256(login.Password) )
            {
                return user.UserId;
            }

            return -1;
        }
        public string getSlugById(int userID)
        {
            using (var context = this._baseContext)
            {
                var result = (from u in context.Users
                           join o in context.Organizations on u.OrganizationId equals o.OrganizationId
                           where (u.UserId == userID)
                           select new { slug = o.Slug }).FirstOrDefault();

                return result == null ? "" : result.slug;
            }
        }

        protected string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();

            byte[] stream = sha256.ComputeHash(encoding.GetBytes(str));
            
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            
            return sb.ToString();
        }
    }
}
