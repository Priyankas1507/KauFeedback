import { useState, useEffect } from "react";
import api from "../services/api";

function FeedbackPage() {
  const [formData, setFormData] = useState({
    patientName: "",
    age: "",
    visitType: "",
    departmentId: "",
    serviceId: "",
    rating: "",
    comments: ""
  });

  const [departments, setDepartments] = useState([]);
  const [services, setServices] = useState([]);

  useEffect(() => {
    loadMasters();
  }, []);

  const loadMasters = async () => {
    try {
      const response = await api.get("/masters");

      setDepartments(response.data.departments);
      setServices(response.data.services);
    } catch (error) {
      console.error(error);
    }
  };

  const filteredServices = services.filter(
    service =>
      service.departmentId === Number(formData.departmentId)
  );

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await api.post("/feedback", {
        patientName: formData.patientName,
        age: Number(formData.age),
        visitType: formData.visitType,
        departmentId: Number(formData.departmentId),
        serviceId: Number(formData.serviceId),
        locationId: 0,
        rating: Number(formData.rating),
        comments: formData.comments
      });

      alert("Feedback Submitted Successfully");

      setFormData({
        patientName: "",
        age: "",
        visitType: "",
        departmentId: "",
        serviceId: "",
        rating: "",
        comments: ""
      });

    } catch (error) {
      console.error(error);
      alert("Failed to submit feedback");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>Patient Feedback Form</h1>

      <form onSubmit={handleSubmit}>
        <div>
          <label>Patient Name</label>
          <br />
          <input
            name="patientName"
            value={formData.patientName}
            onChange={handleChange}
          />
        </div>

        <br />

        <div>
          <label>Age</label>
          <br />
          <input
            type="number"
            name="age"
            value={formData.age}
            onChange={handleChange}
          />
        </div>

        <br />

        <div>
          <label>Visit Type</label>
          <br />
          <input
            name="visitType"
            value={formData.visitType}
            onChange={handleChange}
            placeholder="OP/IP"
          />
        </div>

        <br />

        <div>
          <label>Department</label>
          <br />
          <select
            name="departmentId"
            value={formData.departmentId}
            onChange={handleChange}
          >
            <option value="">Select Department</option>

            {departments.map((department) => (
              <option
                key={department.id}
                value={department.id}
              >
                {department.name}
              </option>
            ))}
          </select>
        </div>

        <br />

        <div>
          <label>Service</label>
          <br />
          <select
            name="serviceId"
            value={formData.serviceId}
            onChange={handleChange}
          >
            <option value="">Select Service</option>

            {filteredServices.map((service) => (
              <option
                key={service.id}
                value={service.id}
              >
                {service.name}
              </option>
            ))}
          </select>
        </div>

        <br />

        <div>
          <label>Rating (1-5)</label>
          <br />
          <input
            type="number"
            min="1"
            max="5"
            name="rating"
            value={formData.rating}
            onChange={handleChange}
          />
        </div>

        <br />

        <div>
          <label>Comments</label>
          <br />
          <textarea
            name="comments"
            value={formData.comments}
            onChange={handleChange}
          />
        </div>

        <br />

        <button type="submit">
          Submit Feedback
        </button>
      </form>
    </div>
  );
}

export default FeedbackPage;