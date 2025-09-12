import { GraphQLClient } from 'graphql-request';
import { API_CONFIG } from '../config/apiConfig';

// Create GraphQL client with default configuration
const graphqlClient = new GraphQLClient(API_CONFIG.GRAPHQL_URL, {
  headers: {
    ...API_CONFIG.DEFAULT_HEADERS,
    'Accept': 'application/json',
  },
  timeout: API_CONFIG.TIMEOUT,
  // credentials: 'include', // Removed to fix CORS issue with wildcard origin
});

// Request interceptor for adding auth tokens, etc.
graphqlClient.setHeaders((headers) => {
  // Add auth token if available
  const token = localStorage.getItem('authToken');
  if (token) {
    return {
      ...headers,
      Authorization: `Bearer ${token}`,
    };
  }
  return headers;
});

// Helper function to handle GraphQL errors
const handleGraphQLError = (error) => {
  console.error('GraphQL Error:', error);
  
  // Handle authentication errors
  if (error.response?.errors?.some(err => err.extensions?.code === 'UNAUTHENTICATED')) {
    localStorage.removeItem('authToken');
    // You can redirect to login page here
  }
  
  // Extract meaningful error message
  if (error.response?.errors?.length > 0) {
    const errorMessage = error.response.errors[0].message;
    throw new Error(errorMessage);
  }
  
  throw error;
};

// Wrapper function for GraphQL requests
export const graphqlRequest = async (query, variables = {}) => {
  try {
    console.log('GraphQL Request:', { query, variables, url: API_CONFIG.GRAPHQL_URL });
    const data = await graphqlClient.request(query, variables);
    console.log('GraphQL Response:', data);
    return data;
  } catch (error) {
    console.error('GraphQL Request Failed:', error);
    handleGraphQLError(error);
  }
};

export default graphqlClient;
