export const API_ENDPOINTS={
    Login:"/api/Auth/signin",
    Signup:"/api/Auth/register",
    VerifyOtp:"/api/Auth/otp/verify",
    ConsumerProfile:"/api/Consumer/profile",
    PostProblem:"/api/Post/problem",
    LogOut:"/api/Auth/signout",
    CreateTechnician:"/api/Technician/create",
    Notification:"/api/Notification",


    //For Technician
    GetPost:"/api/Post/for-technician",
    GetPostById:"api/Post/get",
    CreateBid:"/api/Bid/create",
    ViewBid:"/api/Bid/get/bytechnician",

    GetAllBookings:"/api/Booking/all/Technician",
    CompleteBookings:"/api/Booking/completed",

    //For Consumer
    GetAllPostedProblem:"/api/Post/get/all",
    GetBidById:"api/Bid/get/all",
    BookTechnician:"api/Booking/create/bidBooking",
    GetAllBookingsForConsumer:"/api/Booking/all/consumer",
    CreateReviews:"/api/Review/create",


    //Chatbot
    PostQuestion:"/api/Chatbot/postQuestions",
   
}