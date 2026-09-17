import type { Todo } from '../services/todos'

type TodoCardProps = {
  todo: Todo
  onClick: () => void
}

function TodoCard({ todo, onClick }: TodoCardProps) {
  return (
    <article
      className="todo-card"
      onClick={onClick}
    >
      <h3>{todo.title}</h3>

      {todo.description && (
        <p className="todo-description">{todo.description}</p>
      )}

      <div className="todo-card-footer">
        <span className={`priority priority-${todo.priority.toLowerCase()}`}>
          {todo.priority}
        </span>

        {todo.category && (
          <span className="todo-category">
            {todo.category}
          </span>
        )}
      </div>
    </article>
  )
}

export default TodoCard