# Techno Sewa 🛠️

Techno Sewa is a mobile application designed to bridge the gap between consumers and local technicians in Nepal. It provides a seamless, transparent, and efficient platform for booking verified technicians for services like electrical, plumbing, and appliance repairs. The app includes an AI-powered chatbot for user assistance and supports real-time booking, payment, and feedback.

---
**Frontend Repository**: [Techno Sewa Frontend]([https://github.com/chetanachaudhary/Technosewa-admin.git]

## ✨ Features

### For Consumers:
- **User Registration & Login**: JWT-based authentication.
- **Service Booking**: Book verified technicians for various home services.
- **Location-Based Search**: Find nearby technicians which uses Haversine Formula for distance calculation.
- **AI Chatbot**: Get instant support and troubleshooting help using RAG architecture.
- **Real-Time Notifications**: Receive updates on bookings and bids via SignalR.
- **Payment Integration**: Payments via Khalti.
- **Ratings & Reviews**: Rate and review technicians after service completion.

### For Technicians:
- **Technician Registration**: Apply and get verified by admins.
- **Bid on Posts**: Place bids on consumer service requests.
- **Manage Bookings**: View and update booking status .
- **Earnings & Commission Tracking**: Monitor transactions and commissions.

### For Admins:
- **Dashboard**: Manage users, technicians, services, and bookings.
- **Technician Verification**: Approve or reject technician applications.
- **Analytics**: View system performance and user engagement metrics.

---

## 🛠️ Technologies Used

### Backend:
- **ASP.NET Core Web API** – RESTful APIs
- **Entity Framework Core** – ORM
- **MS SQL Server** – Database
- **JWT** – Authentication
- **SignalR** – Real time notifications
- **Swagger** – API documentation

### Frontend (Mobile):
- **React Native** – Cross-platform mobile app
- **Expo** – Development and testing
- **TypeScript** – Type safe JavaScript

### AI & ML:
- **RAG Architecture** – Retrieval Augmented Generation for chatbot
- **LightGBM with LambdaRank** – Technician ranking algorithm based on proximity and avg ratings
- **Groq API** – LLaMA 3 70B model for NLP tasks
- **Python.NET** – used for enbedding python in .Net

### Tools & Libraries:
- **Visual Studio 2022** – IDE
- **Git** – Version control
- **NGROK** – Tunneling for local testing
- **Pandas** – Data processing for ML
- **Leaflet & OpenStreetMap** – Map integration

---


