﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    /// <summary>
    /// Classe abstrata base para impelmentação dos controllers
    /// </summary>
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ExecuteCommand(ModelResult result)
        {
            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
