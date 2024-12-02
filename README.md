# SOFT1Exam_OrderAndFeedback


## Table of Contents

- [SOFT1Exam\_OrderAndFeedback](#soft1exam_orderandfeedback)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Build Status](#build-status)
  - [CI/CD Pipeline](#cicd-pipeline)
    - [Pipeline Steps](#pipeline-steps)
  - [Tech stack](#tech-stack)
  - [API Documentation](#api-documentation)
    - [Rest](#rest)
    - [RabbitMQ](#rabbitmq)
  - [Docker Compose](#docker-compose)
    - [Overview](#overview)
    - [Dockerhub](#dockerhub)
    - [Services / Containers](#services--containers)

## Introduction

Welcome to the **SOFT1Exam_OrderAndFeedback** repository! This is one of 7 mircoservices for our first semester software PBA exam. This microservice is responsible for managing orders and feedback. This part of the project is the "legacy" part. It's supposed to act as the last parts of the old monolithic system. This microservice functions independently from the other microservices, but it can be used in conjunction with them to create a more complex system.

For this microservice specifically, it is also required that the **SOFT1Exam_Customer** microservice and **SOFT1Exam_Notification** is running, as it is dependent on them.
These can be found here
```
https://github.com/LucasHemm/SOFT1Exam_Customer
https://github.com/LucasHemm/SOFT1Exam_Notification
```

## Build Status
Check out the lastest build status here: ![CI/CD](https://github.com/LucasHemm/SOFT1Exam_OrderAndFeedback/actions/workflows/dotnet-tests.yml/badge.svg)

## CI/CD Pipeline

Our CI/CD pipeline utilizes GitHub Actions to automate the testing and deployment of the application. This uses our whole suite of tests. To initate the pipeline a pull request has to be made to merge the code. After the request has been made the pipeline will run the tests, and deploy the image of the application to dockerhub if all the tests pass.

The pipeline consists of the following steps:

### Pipeline Steps

1. **Checkout Repository**
2. **Setup .NET**
3. **Restore Dependencies**
4. **Build**
5. **Wait for RabbitMQ to be Ready**
6. **Test**
7. **Log in to Docker Hub**
8. **Build Docker Image**
9. **Push Docker Image** 

## Tech stack
The tech stack for this microservice is as follows:
- **C#**: The main programming language for the application.
- **ASP NET Core 8.0**: The main framework for the application.
- **Microsoft SQL Server**: The database for the application.
- **Promehteus**: The library used for metrics.
- **Grafana**: The library used for visualizing the metrics.
- **Docker**: The containerization tool used to deploy the application.
- **Docker Compose**: The tool used to deploy the application locally.
- **GitHub Actions**: The CI/CD tool used to automate the testing and deployment of the application.
- **Swagger**: The library used to document the API.
- **xunit**: The library used for unit and integration testing.
- **Testcontainers**: The library used to create testcontainers for the integration tests.
- **Coverlet**: The library used to create code coverage reports.

## API Documentation
### Rest

| **Endpoint**                  | **Result**                                    | **Format**   |
|-------------------------------|-----------------------------------------------|--------------|
| `POST /api/OrderApi`          | Creates an order                              | JSON         |
| `PUT /api/OrderApi`           | Updates order status                          | JSON         |
| `GET /api/OrderApi`           | Retrieves a list of all Orders                | JSON         |
| `GET /api/OrderApi/{id}`      | Retrieve a order by id                        | JSON         |
| `GET api/OrderApi/status/{status}`| Get list of order by status               | JSON         |
| `PUT /api/OrderApi/updateIds`| Updates Order with agent and payment information| JSON         |
| `GET /api/OrderApi/agent/{agentId}`| Get list of orders by agent              | JSON         |
| `GET /api/OrderApi/customer/{customerId}`|Get finished orders by customer     | JSON         |
| `POST /api/FeedbackApi`       | Creates feedback by order                     | JSON         |
| `GET /api/FeedbackApi/agent/{agentId}`| Get feedbacks by agent                | JSON         |
| `GET /api/FeedbackApi/restaurant/{restaurantId}`| Get feedbacks by restaurant | JSON         |

### RabbitMQ

| **Queue Name** | **Exchange**    | **Routing Key**      | **Description**                                      | **Message Format** |
|----------------|------------------|----------------------|------------------------------------------------------|--------------------|
| email_queue    | default          | email_queue | Sends message with info for when an order status has been updated, so that an email can be sent to the customer | JSON               |




## Docker Compose

### Overview

To run this microservice, you can use Docker Compose to deploy the services locally. 

```yaml
docker-compose up --build
```
To access the Swagger UI and endpoints, navigate to the following URL:
```
http://localhost:8080/swagger/index.html
```

See performance metrics at:
```
http://localhost:8080/metrics
```
Or use the prometheus UI at:
```
http://localhost:9090
```
And the grafana UI at:
```
http://localhost:3000
```

Alternatively, you can run all the services from the MTOGO project by going to this repository and following the guide there.
```
https://github.com/LucasHemm/SOFTEXAM_Deployment
```

### Dockerhub
The docker-compose file uses the local dockerfile to build the image, and run the container. The image can also be found on Docker Hub at:
```
https://hub.docker.com/u/lucashemcph
```

### Services / Containers

- **App** / **Orderandfeedbackservicecontainer**: Runs the main application server.
- **DB** / **Database**: Runs the Microsoft SQL Server database.
- **prometheus** / **prometheus**: Runs the prometheus server.
- **grafana** / **grafana**: Runs the grafana server.







