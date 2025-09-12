import { graphqlRequest } from './api';
import { QUERIES, MUTATIONS } from '../config/apiConfig';

// Task GraphQL API
export const taskApi = {
  // Get all tasks
  getTasks: async () => {
    try {
      const data = await graphqlRequest(QUERIES.GET_ALL_TASKS);
      return data.allTasks;
    } catch (error) {
      console.error('Error fetching tasks:', error);
      throw error;
    }
  },

  // Create a new task
  createTask: async (taskData) => {
    try {
      const input = {
        title: taskData.title,
        description: taskData.description || null,
        status: taskData.status || 'PENDING',
      };
      const data = await graphqlRequest(MUTATIONS.CREATE_TASK, { input });
      return data.createTask;
    } catch (error) {
      console.error('Error creating task:', error);
      throw error;
    }
  },

  // Update task status
  updateTaskStatus: async (id, status) => {
    try {
      const input = {
        id: parseInt(id),
        status: status,
      };
      const data = await graphqlRequest(MUTATIONS.UPDATE_TASK_STATUS, { input });
      return data.updateTaskStatus;
    } catch (error) {
      console.error(`Error updating task ${id} status:`, error);
      throw error;
    }
  },
};

export default taskApi;
