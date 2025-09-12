# API Integration Setup

This document explains how to configure and use the API integration in the React Spectrum Task Manager.

## 🏗️ Architecture

The application is now set up to connect with real APIs instead of using mock data. Here's the structure:

```
src/
├── config/
│   └── apiConfig.js          # API configuration and endpoints
├── services/
│   ├── api.js               # Axios instance with interceptors
│   └── taskApi.js           # Task-specific API calls
├── hooks/
│   └── useTasks.js          # Custom hook for task state management
└── components/
    ├── TaskManager.jsx      # Main component using useTasks hook
    ├── TaskForm.jsx         # Form with API integration
    ├── TaskList.jsx         # Task display component
    └── TaskItem.jsx         # Individual task with API calls
```

## 🔧 Configuration

### API Configuration (`src/config/apiConfig.js`)

Update the API configuration to match your backend:

```javascript
export const API_CONFIG = {
  BASE_URL: 'https://your-api-domain.com/api',  // Your API base URL
  TIMEOUT: 10000,                               // Request timeout
  ENDPOINTS: {
    TASKS: '/tasks',                            // GET all tasks
    TASK_BY_ID: (id) => `/tasks/${id}`,         // GET/PUT/DELETE specific task
    TASK_STATUS: (id) => `/tasks/${id}/status`, // PATCH task status
  },
};
```

## 📡 Expected API Endpoints

Your backend should implement these endpoints:

### 1. GET `/tasks`
**Purpose**: Fetch all tasks
**Response**: Array of task objects
```json
[
  {
    "id": "1",
    "title": "Task title",
    "description": "Task description",
    "status": "PENDING",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-01T00:00:00Z"
  }
]
```

### 2. POST `/tasks`
**Purpose**: Create a new task
**Request Body**:
```json
{
  "title": "New task title",
  "description": "New task description"
}
```
**Response**: Created task object

### 3. PATCH `/tasks/:id/status`
**Purpose**: Update task status
**Request Body**:
```json
{
  "status": "COMPLETED"
}
```
**Response**: Updated task object

### 4. PUT `/tasks/:id` (Optional)
**Purpose**: Update entire task
**Request Body**: Complete task object
**Response**: Updated task object

### 5. DELETE `/tasks/:id` (Optional)
**Purpose**: Delete a task
**Response**: Success message or empty response

## 🔐 Authentication

The API service is configured to handle authentication:

1. **Token Storage**: Tokens are stored in `localStorage` with key `authToken`
2. **Auto-injection**: Tokens are automatically added to request headers
3. **Error Handling**: 401 errors automatically clear stored tokens

To add authentication:
```javascript
// Store token after login
localStorage.setItem('authToken', 'your-jwt-token');

// Token will be automatically included in API requests
```

## 🚀 Usage

### 1. Update API Configuration

Edit `src/config/apiConfig.js` to point to your API:

```javascript
export const API_CONFIG = {
  BASE_URL: 'https://your-api-domain.com/api',
  // ... rest of config
};
```

### 2. Start the Application

```bash
npm run dev
```

### 3. Test API Connection

The application will:
- Show loading state while fetching tasks
- Display error message if API is unreachable
- Allow creating and updating tasks via API calls

## 🔍 Error Handling

The application includes comprehensive error handling:

- **Network Errors**: Displayed in the UI with retry options
- **Validation Errors**: Shown in forms
- **Authentication Errors**: Automatic token cleanup
- **Loading States**: Visual feedback during API calls

## 📝 Task Data Structure

Tasks should follow this structure:

```typescript
interface Task {
  id: string | number;
  title: string;
  description?: string;
  status: 'PENDING' | 'COMPLETED';
  createdAt: string; // ISO date string
  updatedAt: string; // ISO date string
}
```

## 🛠️ Customization

### Adding New API Endpoints

1. Update `src/config/apiConfig.js` with new endpoints
2. Add new methods to `src/services/taskApi.js`
3. Update `src/hooks/useTasks.js` with new functionality

### Modifying Request/Response

1. Update interceptors in `src/services/api.js`
2. Modify error handling in individual API methods
3. Customize loading states in components

## 🔧 Environment Variables

You can use environment variables for configuration:

```bash
# Create .env file in project root
REACT_APP_API_BASE_URL=https://your-api-domain.com/api
```

Then update `src/config/apiConfig.js`:
```javascript
BASE_URL: process.env.REACT_APP_API_BASE_URL || 'http://localhost:3001/api',
```

## 📋 Next Steps

1. **Update API Configuration**: Point to your actual API endpoints
2. **Test API Integration**: Verify all endpoints work correctly
3. **Add Authentication**: Implement your auth flow if needed
4. **Customize Error Messages**: Update error messages for your use case
5. **Add Loading Indicators**: Enhance UX with better loading states

The application is now ready to connect with your APIs! 🚀
