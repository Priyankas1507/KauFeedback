import { useEffect, useState } from "react";
import api from "../services/api";

function DashboardPage() {
  const [dashboard, setDashboard] = useState(null);

  const loadDashboard = async () => {
    try {
      const response = await api.get("/dashboard");
      setDashboard(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    loadDashboard();

    const interval = setInterval(() => {
      loadDashboard();
    }, 30000);

    return () => clearInterval(interval);
  }, []);

  if (!dashboard) {
    return <h2>Loading...</h2>;
  }

  return (
    <div style={{ padding: "20px" }}>
      <h1>Hospital Feedback Dashboard</h1>

      <hr />

      <h2>
        Today's Feedback Count:
        {dashboard.todayFeedbackCount}
      </h2>

      <h2>
        Average Rating:
        {dashboard.averageRating}
      </h2>

      <hr />

      <h2>Department Ratings</h2>

      <ul>
        {dashboard.departmentRatings.map((dept, index) => (
          <li key={index}>
            {dept.department} - {dept.averageRating}
          </li>
        ))}
      </ul>

      <hr />

      <h2>Latest Feedback</h2>

      <table border="1" cellPadding="10">
        <thead>
          <tr>
            <th>Patient</th>
            <th>Rating</th>
            <th>Comments</th>
          </tr>
        </thead>

        <tbody>
          {dashboard.latestFeedbacks.map((feedback) => (
            <tr key={feedback.id}>
              <td>{feedback.patientName}</td>
              <td>{feedback.rating}</td>
              <td>{feedback.comments}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default DashboardPage;