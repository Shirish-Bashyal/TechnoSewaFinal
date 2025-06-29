using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Consumer;
using Application.DTO.User.Post;

namespace Application.DTO.Booking.Technician
{
    public class PendindBookingsDTO
    {
        public PostResponseDTO PostDetails { get; set; }

        public GetBidDTO PostBids { get; set; }
    }
}
