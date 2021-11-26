using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrailersApp.Application.DTOs.Trailer;
using TrailersApp.Application.Interfaces;

namespace TrailersApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TrailersController : ControllerBase
    {
        private readonly ITrailserService _trailserService;

        public TrailersController(ITrailserService trailserService)
        {
            _trailserService = trailserService;
        }

        /// <summary>
        /// Returns all trailers in the database or a list with the jobs filtered by name.
        /// </summary>
        /// <param name="filters">Filters to apply.</param>
        /// <returns>Collection of TrailersListDto.</returns>
        [HttpGet(Name = nameof(GetAllTrailers))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TrailersListDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TrailersListDto>>> GetAllTrailers(
            [FromQuery] string filters)
        {
            var result =  await _trailserService.GetAllTrailers(filters);
            
            return Ok(result);
        }

        /// <summary>
        /// Returns the trailer matching the submited id.
        /// </summary>
        /// <param name="id"> id of the trailer to retreive.</param>
        /// <returns>TrailerDto</returns>
        [HttpGet("{id:int}", Name = nameof(GetTrailers))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrailerDto>> GetTrailers(int id)
        {
            var result = await _trailserService.GetTrailer(id);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new trailer record.
        /// </summary>
        /// <param name="trailerDto">Trailer data.</param>
        /// <returns>New trailer record with its id if success.</returns>
        [HttpPost(Name = nameof(CreateTrailer))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrailerDto>> CreateTrailer(
            [FromBody] CreateTrailerDto trailerDto)
        {
            var result = await _trailserService.AddTrailer(trailerDto);
            return Ok(result);
        }
        
        /// <summary>
        /// Updates an existing trailer.
        /// </summary>
        /// <param name="trailerDto">New trailer data.</param>
        /// <returns>Status code 200 if success.</returns>
        [HttpPut(Name = nameof(UpdateTrailer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTrailer([FromBody] UpdateTrailerDto trailerDto)
        {
            await _trailserService.UpdateTrailer(trailerDto);
            return Ok();
        }
                
        /// <summary>
        /// Deletes an existing trailer record.
        /// </summary>
        /// <param name="id">The trailer id to delete.</param>
        /// <returns>Status code 200 if success.</returns>
        [HttpDelete("{id:int}", Name = nameof(CreateTrailer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteTrailer([FromBody] int id)
        {
            await _trailserService.DeleteTrailer(id);
            return Ok();
        }
    }
}