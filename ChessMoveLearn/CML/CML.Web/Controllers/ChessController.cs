using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CML.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CML.Web.Controllers
{
    [Route("api/chess")]
    [ApiController]
    public class ChessController : ControllerBase
    {
        private readonly ChessDataProvider _DB;
        private readonly ChessService _Serv;

        public ChessController(ChessDataProvider db, ChessService serv)
        {
            _DB = db;
            _Serv = serv;
        }

        // GetPieceTypes
        [HttpGet]
        [AllowAnonymous]
        [Route("GetPieceTypes")]
        public IEnumerable<PieceType> GetPieceTypes()
        {
            return _DB.GetPieces().Select(x => x.Type);
        }

        // GetPieceMoves
        // might help: https://exceptionnotfound.net/serializing-enumerations-in-asp-net-web-api/
        [HttpGet]
        [AllowAnonymous]
        [Route("GetPieceMoves/{pt}")]
        public IEnumerable<Point> GetPieceMoves(PieceType pt)
        {
            var p = _DB.GetPieces().FirstOrDefault(x => x.Type == pt);
            if (null == p)
                return null;

            return p.MoveToPoints;
        }

        // IsMoveValid
        public class IsMoveValidRequestModel
        {
            public PieceType Type { get; set; }
            public Point Coord { get; set; }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("IsMoveValid")]
        public bool IsMoveValid([FromBody]IsMoveValidRequestModel model)
        {
            return _Serv.IsMoveValid(model.Type, null, model.Coord);
        }
    }
}
