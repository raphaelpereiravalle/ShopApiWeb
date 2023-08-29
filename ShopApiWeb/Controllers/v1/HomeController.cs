using Microsoft.AspNetCore.Mvc;
using ShopApiWeb.Models;
using ShopApiWeb.Repositories;
using ShopApiWeb.Services;

namespace ShopApiWeb.Controllers.v1
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public HomeController(TokenService tokenService)
        {
            _tokenService = tokenService ;
        }

        [HttpPost]
        [Route("v1/login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] User model)
        {
            // Recupera o usuário
            var user = UserRepository.Get(model.Username, model.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Tokenuio
            string token = _tokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dadosukiu
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
