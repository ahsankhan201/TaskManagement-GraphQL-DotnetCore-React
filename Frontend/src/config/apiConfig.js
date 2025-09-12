// GraphQL API Configuration
export const API_CONFIG = {
  // GraphQL endpoint URL
  GRAPHQL_URL: import.meta.env.VITE_GRAPHQL_URL || 'http://localhost:7220/graphql',
  
  // Timeout for requests (in milliseconds)
  TIMEOUT: 10000,
  
  // Default headers for GraphQL requests
  DEFAULT_HEADERS: {
    'Content-Type': 'application/json',
  },
};

// GraphQL Queries
export const QUERIES = {
  GET_ALL_TASKS: `
    query GetAllTasks {
      allTasks {
        id
        title
        description
        status
        createdAt
      }
    }
  `,
};

// GraphQL Mutations
export const MUTATIONS = {
  CREATE_TASK: `
    mutation CreateTask($input: CreateTaskInput!) {
      createTask(input: $input) {
        id
        title
        description
        status
        createdAt
      }
    }
  `,
  
  UPDATE_TASK_STATUS: `
    mutation UpdateTaskStatus($input: UpdateTaskStatusInput!) {
      updateTaskStatus(input: $input) {
        id
        title
        description
        status
        createdAt
        updatedAt
      }
    }
  `,
};

export default API_CONFIG;
