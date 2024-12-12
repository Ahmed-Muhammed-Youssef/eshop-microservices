# Eshop Microservices Project

Welcome to the **Eshop Microservices** project! This repository contains the source code and documentation for a distributed e-commerce platform built using **C#** and modern microservices architecture.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Services](#services)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [License](#license)

---

## Overview

The **Eshop Microservices** project is a cloud-native e-commerce solution designed to demonstrate the principles of microservices architecture. It uses a modular approach to decouple features like catalog management, ordering, and payment into separate services.

---

## Features

- **Distributed Services:** Independent and scalable microservices for core functionalities.
- **API Gateway:** Centralized API gateway for routing and aggregation.
- **Event-Driven Architecture:** Services communicate using asynchronous messaging.
- **Containerization:** Dockerized services for easy deployment.
- **Database Per Service:** Each service manages its own data store.
- **Resiliency:** Fault-tolerant design with retry policies and circuit breakers.

---

## Architecture

Below is a high-level diagram of the architecture:

```text
[Client] ---> [API Gateway] ---> [Microservices (Catalog, Ordering, Discount, Basket, Payment)]
                                |--> [Message Broker (RabbitMQ)]
                                |--> [Databases (PostgreSQL, SQL Server, Redis)]
```

### Key Components:

- **Catalog Service:** Manages product listings and inventory.
- **Basket Service:** Processes carts/baskets.
- **Ordering Service:** Handles order processing and workflows.
- **Discount Service:** Handles Discounts workflow.

---

## Services

| Service           | Description                         |    Architecture    | local Port |
|-------------------|-------------------------------------|--------------------|------------|
| Catalog           | Manages product catalog             |   Vertical Slice   |    5050    |
| Basket            | Handles Baskets Creation and updates|                    |    5051    |
| Ordering          | Handles order creation and updates  |                    |    5053    |
| Discount          | Processes discounts                 |                    |            |

---

## Technologies

- **Backend:** .NET 8, C#
- **Messaging:** RabbitMQ
- **Databases:** PostgreSQL, SQL Server and Redis
- **Containerization:** Docker, Docker Compose
- **API Gateway:** Yarp

---

## Getting Started

### Prerequisites

Ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Ahmed-Muhammed-Youssef/eshop-microservices.git
   cd eshop-microservices
   ```

2. Build and run the containers using Docker Compose:
   ```bash
   docker-compose up --build
   ```

---

## To-Do List

- [x] Implement Catalog Service
- [ ] Implement Ordering Service
- [ ] Implement Discount Service
- [ ] Implement Basket Service
- [ ] Implement Payment Service

---

## License

This project is licensed under the [MIT License](LICENSE).

