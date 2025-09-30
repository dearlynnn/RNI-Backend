using Microsoft.AspNetCore.Mvc;
using UninunezRNIBackend.Models.DTO;
using UninunezRNIBackend.Services;

namespace UninunezRNIBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesConvenioController : ControllerBase
    {
        private readonly IAgreementRequestService _agreementRequestService;

        public SolicitudesConvenioController(IAgreementRequestService agreementRequestService)
        {
            _agreementRequestService = agreementRequestService;
        }

        /// <summary>
        /// Crear nueva solicitud de convenio
        /// </summary>
        /// <param name="createDto">Datos de la nueva solicitud</param>
        /// <returns>ID de la solicitud creada</returns>
        [HttpPost]
        public async Task<IActionResult> CreateSolicitudConvenio([FromBody] CreateAgreementRequestDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var solicitudId = await _agreementRequestService.CreateAsync(createDto);
                
                return CreatedAtAction(
                    nameof(GetSolicitudConvenioSolicitante), 
                    new { id = solicitudId, requesterEmail = createDto.ProposerEmail }, 
                    new { id = solicitudId, message = "Solicitud creada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Consulta de Solicitud de Convenio
        /// Permite al solicitante consultar el estado actual de su solicitud de registro de convenio o adhesión a una red.
        /// </summary>
        /// <param name="id">ID único de la solicitud</param>
        /// <param name="requesterEmail">Email del solicitante (query parameter opcional)</param>
        /// <param name="isRniUser">Indica si el usuario es del personal RNI (query parameter opcional)</param>
        /// <returns>Información de la solicitud según el tipo de usuario</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSolicitudConvenio(
            Guid id, 
            [FromQuery] string? requesterEmail = null,
            [FromQuery] bool isRniUser = false)
        {
            try
            {
                // Si es usuario RNI, devolver vista completa
                if (isRniUser)
                {
                    var rniResult = await _agreementRequestService.GetForRniAsync(id);
                    if (rniResult == null)
                    {
                        return NotFound(new { message = "Solicitud no encontrada" });
                    }
                    return Ok(rniResult);
                }

                // Si es solicitante, verificar autorización y devolver vista pública
                if (string.IsNullOrEmpty(requesterEmail))
                {
                    return BadRequest(new { message = "Se requiere el email del solicitante para acceder a la solicitud" });
                }

                // Verificar que el solicitante puede acceder a esta solicitud
                var isAuthorized = await _agreementRequestService.IsRequesterAuthorizedAsync(id, requesterEmail);
                if (!isAuthorized)
                {
                    return Forbid("No tiene permisos para acceder a esta solicitud");
                }

                var publicResult = await _agreementRequestService.GetForRequesterAsync(id, requesterEmail);
                if (publicResult == null)
                {
                    return NotFound(new { message = "Solicitud no encontrada" });
                }

                return Ok(publicResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint específico para personal RNI - vista completa de la solicitud
        /// </summary>
        /// <param name="id">ID único de la solicitud</param>
        /// <returns>Vista completa de la solicitud con información sensible</returns>
        [HttpGet("rni/{id:guid}")]
        public async Task<IActionResult> GetSolicitudConvenioRni(Guid id)
        {
            try
            {
                var result = await _agreementRequestService.GetForRniAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = "Solicitud no encontrada" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint específico para solicitantes - vista pública de su propia solicitud
        /// </summary>
        /// <param name="id">ID único de la solicitud</param>
        /// <param name="requesterEmail">Email del solicitante</param>
        /// <returns>Vista pública de la solicitud</returns>
        [HttpGet("solicitante/{id:guid}")]
        public async Task<IActionResult> GetSolicitudConvenioSolicitante(
            Guid id, 
            [FromQuery] string requesterEmail)
        {
            try
            {
                if (string.IsNullOrEmpty(requesterEmail))
                {
                    return BadRequest(new { message = "Se requiere el email del solicitante" });
                }

                // Verificar autorización
                var isAuthorized = await _agreementRequestService.IsRequesterAuthorizedAsync(id, requesterEmail);
                if (!isAuthorized)
                {
                    return Forbid("No tiene permisos para acceder a esta solicitud");
                }

                var result = await _agreementRequestService.GetForRequesterAsync(id, requesterEmail);
                if (result == null)
                {
                    return NotFound(new { message = "Solicitud no encontrada" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}